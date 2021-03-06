﻿using System;
using System.Collections.Generic;

namespace FbxSharp
{
    public abstract class Property
    {
        public Property(string name)
        {
            Name = name;
        }

        public string Name { get; protected set; }
        public abstract Type PropertyDataType { get; }

        public abstract T Get<T>();
        public abstract object GetValue();
        public abstract void Set<T>(T value);
        public abstract void Set(object value);

        //public Object FbxObject { get; protected set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, GetValue());
        }

        public List<FbxObject> SrcObjects = new List<FbxObject>();

        public bool ConnectSrcObject(FbxObject src, Connection.EType type=Connection.EType.None)
        {
            SrcObjects.Add(src);
            return true;
        }
    }
}

