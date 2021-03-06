﻿using System;
using System.Collections.Generic;

namespace FbxSharp
{
    public class FbxObject : Emitter
    {
        public override string ToString()
        {
            return string.Format("[{2}: Name={0}, UniqueId={1}]", Name, UniqueId, this.GetType().Name);
        }

        public List<FbxObject> SrcObjects = new List<FbxObject>();

        public void ConnectSrcObject(FbxObject fbxObject, Connection.EType type = Connection.EType.None)
        {
            SrcObjects.Add(fbxObject);
        }

        public List<Property> Properties = new List<Property>();

        public Property FindProperty(string name, bool caseSensitive=true)
        {
            return Properties.Find(p =>
                string.Compare(p.Name, name, ignoreCase: !caseSensitive) == 0
            );
        }

        public Property FindProperty(string name, Type type, bool caseSensitive=true)
        {
            return Properties.Find(p => {
                if (p.PropertyDataType != type) return false;

                return string.Compare(p.Name, name, ignoreCase: !caseSensitive) == 0;
            });
        }
        public PropertyT<T> FindProperty<T>(string name, bool caseSensitive=true)
        {
            return (PropertyT<T>)Properties.Find(p => {
                if (!(p is PropertyT<T>)) return false;

                return string.Compare(p.Name, name, ignoreCase: !caseSensitive) == 0;
            });
        }

        public PropertyT<T> CreateProperty<T>(string name)
        {
            var prop = new PropertyT<T>(name);
            Properties.Add(prop);
            return prop;
        }
        public Property CreateProperty(string name, Type type)
        {
            var concreteType = typeof(PropertyT<>).MakeGenericType(type);
            var prop = (Property)Activator.CreateInstance(concreteType, (object)name);
            Properties.Add(prop);
            return prop;
        }


        public string Name { get; set; }

        //Returns the unique ID of this object.
        public ulong UniqueId { get; set; } //protected set; }
    }
}

