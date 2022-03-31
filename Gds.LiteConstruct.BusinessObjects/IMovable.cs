using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public interface IMovable
    {
        void MoveTo(Vector3 pos);
        void MoveBy(Vector3 mpos);

        void MoveByX(float mx);
        void MoveByY(float my);
        void MoveByZ(float mz);

        void MoveX(float x);
        void MoveY(float y);
        void MoveZ(float z);

        Vector3 Position { get; set; }
    }
}
