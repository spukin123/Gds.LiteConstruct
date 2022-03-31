using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class ObjectsBuffer<Type>
    {
        private List<Type> objects = new List<Type>();

        public int ItemsCount
        {
            get { return objects.Count; } 
        }

        public ObjectsBuffer(Type[] objects)
        {
            this.objects.AddRange(objects);
        }

        public ObjectsBuffer(List<Type> objects)
        {
            this.objects = objects;
        }

        public ObjectsBuffer()
        {
        }

        public void AddItem(Type newItem)
        {
            objects.Add(newItem);
        }

        public Type this[int index]
        {
            get
            {
                if (index <= objects.Count)
                {
                    return objects[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

            set
            {
                if (index <= objects.Count)
                {
                    objects[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
