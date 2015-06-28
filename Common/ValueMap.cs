﻿#define UNITY_ENGINE

using System;
using System.Collections.Generic;

#if UNITY_ENGINE
using UnityEngine;
using System.IO;
#endif

namespace FreeUniverse.Common
{
    public struct Value
    {
        public object obj { get; set; }

        public bool IsTypeof<T>()
        {
            if (IsEmpty())
                return false;

            return obj.GetType() == typeof(T);
        }

        public bool IsEmpty()
        {
            return obj == null;
        }

        public static T Cast<T>(Value v)
        {
            if (!v.IsTypeof<T>())
                throw new Exception("Value has invalid type");

            return (T)v.obj;
        }

        public T Cast<T>()
        {
            if (!IsTypeof<T>())
                throw new Exception("Value has invalid type");

            return (T)obj;
        }

        public Value(object obj)
            : this()
        {
            this.obj = obj;
        }

        public static implicit operator Value(int i) { return new Value(i); }
        public static implicit operator int(Value val) { return Cast<int>(val); }

        public static implicit operator Value(float i) { return new Value(i); }
        public static implicit operator float(Value val) { return Cast<float>(val); }

        public static implicit operator Value(uint i) { return new Value(i); }
        public static implicit operator uint(Value val) { return Cast<uint>(val); }

        public static implicit operator Value(ulong i) { return new Value(i); }
        public static implicit operator ulong(Value val) { return Cast<ulong>(val); }

        public static implicit operator Value(string i) { return new Value(i); }
        public static implicit operator string(Value val) { return Cast<string>(val); }

        public static implicit operator Value(Value[] i) { return new Value(i); }
        public static implicit operator Value[](Value val) { return Cast<Value[]>(val); }

        public static implicit operator Value(ValueMap i) { return new Value(i); }
        public static implicit operator ValueMap(Value val) { return Cast<ValueMap>(val); }

        public static implicit operator Value(uidkey i) { return new Value(i); }
        public static implicit operator uidkey(Value val) { return Cast<uidkey>(val); }


#if UNITY_ENGINE
        public static implicit operator Value(Color i) { return new Value(i); }
        public static implicit operator Color(Value val) { return Cast<Color>(val); }

        public static implicit operator Value(Vector2 i) { return new Value(i); }
        public static implicit operator Vector2(Value val) { return Cast<Vector2>(val); }

        public static implicit operator Value(Vector3 i) { return new Value(i); }
        public static implicit operator Vector3(Value val) { return Cast<Vector3>(val); }

        public static implicit operator Value(Quaternion i) { return new Value(i); }
        public static implicit operator Quaternion(Value val) { return Cast<Quaternion>(val); }

        public static implicit operator Value(Transform i) { return new Value(i); }
        public static implicit operator Transform(Value val) { return Cast<Transform>(val); }

        public static implicit operator Value(UnityEngine.Object i) { return new Value(i); }
        public static implicit operator UnityEngine.Object(Value val) { return Cast<UnityEngine.Object>(val); }
#endif

        enum ValueTypeID
        {
            Null,
            Int,
            Float,
            UInt,
            ULong,
            String,
            Array,
            ValueMap,
            UIDKey,

#if UNITY_ENGINE
            Color,
            Vector2,
            Vector3,
            Quaternion,
            Transform,
#endif
            Unknown
        }

        public void Write(BinaryWriter writer)
        {
            if (obj == null)
                return;

            Type type = obj.GetType();

            if (type == typeof(uidkey))
            {
                writer.Write((byte)ValueTypeID.UIDKey);
                uidkey k = Cast<uidkey>();
                writer.Write(k.a);
                writer.Write(k.b);
                writer.Write(k.c);
                writer.Write(k.d);
                return;
            }

            if (type == typeof(int))
            {
                writer.Write((byte)ValueTypeID.Int);
                writer.Write(Cast<int>());
                return;
            }

            if (type == typeof(float))
            {
                writer.Write((byte)ValueTypeID.Float);
                writer.Write(Cast<float>());
                return;
            }

            if (type == typeof(uint))
            {
                writer.Write((byte)ValueTypeID.UInt);
                writer.Write(Cast<uint>());
                return;
            }

            if (type == typeof(ulong))
            {
                writer.Write((byte)ValueTypeID.ULong);
                writer.Write(Cast<ulong>());
                return;
            }

            if (type == typeof(string))
            {
                writer.Write((byte)ValueTypeID.String);
                writer.Write(Cast<string>());
                return;
            }

            if (type == typeof(Value[]))
            {
                writer.Write((byte)ValueTypeID.Array);

                Value[] array = Cast<Value[]>();

                writer.Write((int)array.Length);

                for (int i = 0; i < array.Length; i++)
                    array[i].Write(writer);

                return;
            }

            if (type == typeof(ValueMap))
            {
                writer.Write((byte)ValueTypeID.ValueMap);
                byte[] data = Cast<ValueMap>().ToByteArray();
                writer.Write((int)data.Length);
                writer.Write(data);
                return;
            }

#if UNITY_ENGINE
            if (type == typeof(Color))
            {
                writer.Write((byte)ValueTypeID.Color);

                Color c = Cast<Color>();

                writer.Write(c.r);
                writer.Write(c.g);
                writer.Write(c.b);
                writer.Write(c.a);
            }

            if (type == typeof(Vector2))
            {
                writer.Write((byte)ValueTypeID.Vector2);

                Vector2 v = Cast<Vector2>();

                writer.Write(v.x);
                writer.Write(v.y);
            }

            if (type == typeof(Vector3))
            {
                writer.Write((byte)ValueTypeID.Vector3);

                Vector3 v = Cast<Vector3>();

                writer.Write(v.x);
                writer.Write(v.y);
                writer.Write(v.z);
            }

            if (type == typeof(Quaternion))
            {
                writer.Write((byte)ValueTypeID.Quaternion);

                Quaternion v = Cast<Quaternion>();

                writer.Write(v.x);
                writer.Write(v.y);
                writer.Write(v.z);
                writer.Write(v.w);
            }
#endif

        }

        public void Read(BinaryReader reader)
        {
            if (!IsEmpty())
                throw new Exception("trying to write to non empty value");

            ValueTypeID id = (ValueTypeID)reader.ReadByte();

            switch (id)
            {
                case ValueTypeID.Int:
                    {
                        this.obj = reader.ReadInt32();
                    }
                    break;
                case ValueTypeID.ULong:
                    {
                        this.obj = reader.ReadUInt64();
                    }
                    break;
                case ValueTypeID.UInt:
                    {
                        this.obj = reader.ReadUInt32();
                    }
                    break;
                case ValueTypeID.Float:
                    {
                        this.obj = reader.ReadSingle();
                    }
                    break;
                case ValueTypeID.String:
                    {
                        this.obj = reader.ReadString();
                    }
                    break;
                case ValueTypeID.ValueMap:
                    {
                        this.obj = new ValueMap(reader.ReadBytes(reader.ReadInt32()));
                    }
                    break;
                case ValueTypeID.UIDKey:
                    {
                        uint a = reader.ReadUInt32();
                        uint b = reader.ReadUInt32();
                        uint c = reader.ReadUInt32();
                        uint d = reader.ReadUInt32();

                        this.obj = new uidkey(a, b, c, d);
                    }
                    break;
                case ValueTypeID.Array:
                    {
                        int len = reader.ReadInt32();

                        Value[] array = new Value[len];

                        for (int i = 0; i < len; i++)
                        {
                            array[i] = new Value();
                            array[i].Read(reader);
                        }

                        this.obj = array;
                    }
                    break;
#if UNITY_ENGINE
                case ValueTypeID.Color:
                    {
                        float r = reader.ReadSingle();
                        float g = reader.ReadSingle();
                        float b = reader.ReadSingle();
                        float a = reader.ReadSingle();

                        this.obj = new Color(r, g, b, a);
                    }
                    break;

                case ValueTypeID.Vector2:
                    {
                        float x = reader.ReadSingle();
                        float y = reader.ReadSingle();

                        this.obj = new Vector2(x, y);
                    }
                    break;

                case ValueTypeID.Vector3:
                    {
                        float x = reader.ReadSingle();
                        float y = reader.ReadSingle();
                        float z = reader.ReadSingle();

                        this.obj = new Vector3(x, y, z);
                    }
                    break;

                case ValueTypeID.Quaternion:
                    {
                        float x = reader.ReadSingle();
                        float y = reader.ReadSingle();
                        float z = reader.ReadSingle();
                        float w = reader.ReadSingle();

                        this.obj = new Quaternion(x, y, z, w);
                    }
                    break;
#endif
                default:
                    throw new Exception("unsupported value type = " + (byte)id);
            }
        }
    }

    public class ValueMap
    {
        private Dictionary<string, Value> values { get; set; }

        public ValueMap()
        {
            values = new Dictionary<string, Value>();
        }

        public ValueMap(string fromBase64)
            : this(Convert.FromBase64String(fromBase64))
        {

        }

        public ValueMap(byte[] serializedMap)
            : this()
        {
            FromByteArray(serializedMap);
        }

        public Value this[string key]
        {
            get
            {
                Value val;
                values.TryGetValue(key, out val);
                return val;
            }
            set
            {
                values[key] = value;
            }
        }

        public void FromByteArray(byte[] data)
        {
            // MH: wtf?
            //if (values.Count == 0)
            //    throw new Exception("value map is not empty, use Merge from other map");

            MemoryStream stream = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(stream);

            while (stream.Position < stream.Length)
            {
                string key = reader.ReadString();
                Value v = new Value();
                v.Read(reader);
                values[key] = v;
            }
        }

        public void Merge(ValueMap other, bool overwrite)
        {
            if (overwrite)
            {
                foreach (KeyValuePair<string, Value> e in other.values)
                    this.values[e.Key] = e.Value;
            }
            else
            {
                foreach (KeyValuePair<string, Value> e in other.values)
                {
                    if (this.values.ContainsKey(e.Key))
                        continue;

                    this.values[e.Key] = e.Value;
                }
            }
        }

        public byte[] ToByteArray()
        {
            if (values.Count == 0)
                return null;

            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memoryStream);

            foreach (KeyValuePair<string, Value> e in values)
            {
                writer.Write(e.Key);
                e.Value.Write(writer);
            }

            return memoryStream.ToArray();
        }

        public string ToBase64String()
        {
            if (values.Count == 0)
                return null;

            return Convert.ToBase64String(ToByteArray());
        }
    }
}