using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using System.Drawing;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors
{
    public class RotationRingInteractor : RectangleMouseInteractor3D
    {
        protected Vector3 normalVec = new Vector3(0f, 1f, 0f);

        private Vector3 look;        
        public Vector3 Look 
        {
            get { return look; }
            set 
            { 
                look = value; 
                MakeBillboard();
            }
        }

        public RotationRingInteractor(Vector3 position, Size2 size, Bitmap textureBitmap, Color color) : base(size, color, textureBitmap, position)
        {
            InitBuffers();
        }

        private void MakeBillboard()
        {
            SquareBillboard billboard;
            billboard = SquareBillboard.FromNormalVector(-look);

            CustomVertex.PositionColoredTextured[] vertices = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, 0);

            Vector3 upVec, sideVec;
            upVec = billboard.UpVec * size.Height;
            sideVec = billboard.SideVec * size.Width;
            
            vertices[0].Position = upVec + sideVec;
            vertices[1].Position = upVec - sideVec;
            vertices[2].Position = -upVec - sideVec;
            vertices[3].Position = -upVec + sideVec;

            etalonIS[0].Point1 = vertices[0].Position;
            etalonIS[0].Point2 = vertices[1].Position;
            etalonIS[0].Point3 = vertices[2].Position;
            etalonIS[1].Point1 = vertices[2].Position;
            etalonIS[1].Point2 = vertices[3].Position;
            etalonIS[1].Point3 = vertices[0].Position;

            vertexBuffer.Unlock();
        }

        private void RestoreState(object sender, EventArgs e)
        {
            MakeBillboard();
        }

        protected override void ConnectToBuffersEvents()
        {
            base.ConnectToBuffersEvents();
            vertexBuffer.Created += new EventHandler(RestoreState);
        } 
    }
}
