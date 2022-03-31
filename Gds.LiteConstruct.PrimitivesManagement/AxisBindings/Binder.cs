using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Axises;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.LiteConstruct.BusinessObjects;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.PrimitivesManagement.AxisBindings
{
    internal class Binder
    {
        private TransformationSmoother smoother;
        private AxisAngle rotationValue;
        private Vector3 translationVector;

        private Axis staticAxis;
        private FreeBindingAxis dynamicAxis;

        private const int SmoothTime = 400;

        private AxisAngle startRotation;
        private Vector3 startPosition;

        public Binder()
        {
        }
        
        public void BindFreeAxisToAssociated(AssociatedBindingAxis staticAxis, FreeBindingAxis dynamicAxis)
        {
            this.staticAxis = staticAxis;
            this.dynamicAxis = dynamicAxis;

            FindTransformationParams();
            
            smoother = new TransformationSmoother(dynamicAxis.Container, dynamicAxis.Container, translationVector, rotationValue, SmoothTime);
            smoother.SmoothFinished += Smoother_SmoothFinishedForMixedAxes;
            smoother.Smooth();
        }

        public void BindFreeAxisToFreeAxis(FreeBindingAxis staticAxis, FreeBindingAxis dynamicAxis)
        {
            this.staticAxis = staticAxis;
            this.dynamicAxis = dynamicAxis;

            FindTransformationParams();

            smoother = new TransformationSmoother(dynamicAxis.Container, dynamicAxis.Container, translationVector, rotationValue, SmoothTime);
            smoother.SmoothFinished += Smoother_SmoothFinishedForFreeAxes;
            smoother.Smooth();
        }

        public static bool CanBind(Axis staticAxis, FreeBindingAxis dynamicFreeAxis)
        {
            RotationVector rotation;
            rotation = FindRotationVector(dynamicFreeAxis.Body, staticAxis.Body).ToRotationVector();

            ValuesComparer.Precision = 0.0000001f;
            if (!ValuesComparer.FloatValuesEqual(rotation.X.Radians, 0f))
            {
                if (!dynamicFreeAxis.CanRotateX)
                {
                    return false;
                }
            }

            if (!ValuesComparer.FloatValuesEqual(rotation.Y.Radians, 0f))
            {
                if (!dynamicFreeAxis.CanRotateY)
                {
                    return false;
                }
            }

            if (!ValuesComparer.FloatValuesEqual(rotation.Z.Radians, 0f))
            {
                if (!dynamicFreeAxis.CanRotateZ)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CanAssociatedPrimitiveBeDynamic()
        {
            return false;
        }

        public static bool CanFreePrimitiveBeDynamic()
        {
            return true;
        }

        #region Private Interface

        private void FindTransformationParams()
        {
            startRotation = dynamicAxis.Container.Rotation.ToQuaternion();
            startPosition = dynamicAxis.Container.Position;

            FindRotation();
            FindTranslation();
        }

        private void Smoother_SmoothFinishedForMixedAxes()
        {
            smoother.SmoothFinished -= Smoother_SmoothFinishedForMixedAxes;

            RotateFinally();
            TranslateFinally();

            Axis newAxis;
            newAxis = GetNewAxisParams(staticAxis, dynamicAxis);

            AssociatedBindingAxis staticAssociatedAxis;
            staticAssociatedAxis = staticAxis as AssociatedBindingAxis;

            staticAssociatedAxis.Origin = newAxis.Origin;
            staticAssociatedAxis.Body = newAxis.Body;
            staticAssociatedAxis.Radius = newAxis.Radius;
            staticAssociatedAxis.ConnectFreeAxis(dynamicAxis.Id);
            staticAssociatedAxis.ConnectPrimitive(dynamicAxis.Container.Id);

            RaiseGeometricBindingFinishedForMixedAxes();
        }

        private void Smoother_SmoothFinishedForFreeAxes()
        {
            smoother.SmoothFinished -= Smoother_SmoothFinishedForFreeAxes;

            RotateFinally();
            TranslateFinally();

            Axis newAxis;
            newAxis = GetNewAxisParams(staticAxis, dynamicAxis);

            FreeBindingAxis staticFreeAxis;
            staticFreeAxis = staticAxis as FreeBindingAxis;

            AssociatedBindingAxis result;
            result = new AssociatedBindingAxis(newAxis.Origin, newAxis.Body, newAxis.Radius, staticFreeAxis.Container.Id, staticFreeAxis.Id, dynamicAxis.Container.Id, dynamicAxis.Id);

            RaiseGeometricBindingFinishedForFreeAxes(result);
        }

        private static Axis GetNewAxisParams(Axis axis1, FreeBindingAxis axis2)
        {
            float radius;
            if (axis1.Radius >= axis2.Radius)
            {
                radius = axis1.Radius;
            }
            else
            {
                radius = axis2.Radius;
            }

            Vector3 body, origin;
            Axis transformedDynamicAxis = axis2.Container.GetAxisById(axis2.Id);
            if (transformedDynamicAxis.Body.Length() >= axis1.Body.Length())
            {
                origin = transformedDynamicAxis.Origin;
                body = transformedDynamicAxis.Body;
            }
            else
            {
                origin = axis1.Origin;
                body = axis1.Body;
            }

            return new Axis(origin, body, radius);
        }

        private static AxisAngle FindRotationVector(Vector3 startVec, Vector3 targetVec)
        {
            if (!Vector3Utils.VectorsHaveSameDirection(startVec, targetVec))
            {
                targetVec *= -1f;
            }

            return Vector3Utils.TransitionRotationByAxis(startVec, targetVec);
        }

        private FreeBindingAxis RotateFreeDynamicAxis(FreeBindingAxis dynamicAxis)
        {
            Vector3 dynamicPrimitiveCenter;
            dynamicPrimitiveCenter = dynamicAxis.Container.Position;

            Vector3 axisStartPoint, axisEndPoint;
            axisStartPoint = dynamicAxis.Origin - dynamicPrimitiveCenter;
            axisEndPoint = axisStartPoint + dynamicAxis.Body;

            Matrix rotation;
            rotation = rotationValue.GetRotationMatrix();

            axisStartPoint = Vector3.TransformCoordinate(axisStartPoint, rotation);
            axisEndPoint = Vector3.TransformCoordinate(axisEndPoint, rotation);

            dynamicAxis.Origin = axisStartPoint + dynamicPrimitiveCenter;
            dynamicAxis.Body = (axisEndPoint + dynamicPrimitiveCenter) - dynamicAxis.Origin;

            return dynamicAxis;
        }

        private void RotateFinally()
        {
            AxisAngle rotationCombination;
            PrimitiveBase dynamicPrimitive;

            dynamicPrimitive = dynamicAxis.Container;
            rotationCombination = startRotation * rotationValue;

            dynamicPrimitive.Rotation = rotationCombination.ToRotationVector();
        }

        private void TranslateFinally()
        {
            dynamicAxis.Container.MoveTo(startPosition + translationVector);
        }

        private void FindRotation()
        {
            rotationValue = FindRotationVector(dynamicAxis.Body, staticAxis.Body);
        }

        private void FindTranslation()
        {
            Vector3 staticCenter, dynamicCenter;
            staticCenter = staticAxis.Origin + staticAxis.Body * 0.5f;

            FreeBindingAxis rotatedDynamicAxis;
            rotatedDynamicAxis = RotateFreeDynamicAxis(dynamicAxis);

            dynamicCenter = rotatedDynamicAxis.Origin + rotatedDynamicAxis.Body * 0.5f;
            translationVector = staticCenter - dynamicCenter;
        }

        private void RaiseGeometricBindingFinishedForFreeAxes(AssociatedBindingAxis associatedAxis)
        {
            if (GeometricBindingFinishedForFreeAxes != null)
            {
                GeometricBindingFinishedForFreeAxes(associatedAxis);
            }
        }

        private void RaiseGeometricBindingFinishedForMixedAxes()
        {
            if (GeometricBindingFinishedForMixedAxes != null)
            {
                GeometricBindingFinishedForMixedAxes();
            }
        }

        #endregion

        public event FreeAxesBindedHandler GeometricBindingFinishedForFreeAxes;
        public event NotifyHandler GeometricBindingFinishedForMixedAxes;

        public delegate void FreeAxesBindedHandler(AssociatedBindingAxis associatedAxis);
    }
}
