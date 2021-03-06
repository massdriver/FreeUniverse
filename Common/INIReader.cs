﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common
{
    public sealed class INIReaderParameter
    {
        public string nickname;
        public string[] values;

        public INIReaderParameter(string[] values)
        {
            this.nickname = values[0];
            this.values = values;
        }

        public static bool operator ==(INIReaderParameter param, string s)
        {
            return param.nickname.CompareTo(s) == 0;
        }

        public static bool operator !=(INIReaderParameter param, string s)
        {
            return param.nickname.CompareTo(s) != 0;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string GetName()
        {
            return values[0];
        }

        public bool Check(string name)
        {
            return nickname.CompareTo(name) == 0;
        }

        public T Get<T>(int id)
        {
            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(GetString(id), typeof(T));
            else if(typeof(T) == typeof(int))
                return (T)Convert.ChangeType(GetInt(id), typeof(T));
            else if (typeof(T) == typeof(uint))
                return (T)Convert.ChangeType(GetStrkey(id), typeof(T));
            else if (typeof(T) == typeof(ulong))
                return (T)Convert.ChangeType(GetStrkey64(id), typeof(T));
            else if (typeof(T) == typeof(bool))
                return (T)Convert.ChangeType(GetBool(id), typeof(T));
            else if (typeof(T) == typeof(float))
                return (T)Convert.ChangeType(GetFloat(id), typeof(T));
            else if (typeof(T) == typeof(Vector2))
                return (T)Convert.ChangeType(GetVector2(id), typeof(T));
            else if (typeof(T) == typeof(Vector3))
                return (T)Convert.ChangeType(GetVector3(id), typeof(T));
            else if (typeof(T) == typeof(Vector4))
                return (T)Convert.ChangeType(GetVector4(id), typeof(T));

            return default(T);
        }

        public string GetString(int id)
        {
            if (values.Length <= (id + 1))
                return null;

            return values[id + 1];
        }

        public uint GetStrkey(int id)
        {
            if (values.Length <= (id + 1))
                return 0;

            string s = values[id + 1];

            if (s.CompareTo("null") == 0)
                return 0;

            return Hash.FromString(s);
        }

        public ulong GetStrkey64(int id)
        {
            if (values.Length <= (id + 1))
                return 0;

            string s = values[id + 1];

            if (s.CompareTo("null") == 0)
                return 0;

            return Hash.FromString64(s);
        }

        public int GetInt(int id)
        {
            if (values.Length <= (id + 1))
                return 0;

            return System.Int32.Parse(values[id + 1]);
        }

        public bool GetBool(int id)
        {
            try
            {
                if (GetInt(id) == 1) return true;
            }
            catch (System.FormatException e)
            {
                if (GetString(id).CompareTo("true") == 0) return true;
            }

            return false;
        }

        public float GetFloat(int id)
        {
            if (values.Length <= (id + 1))
                return 0.0f;

            return (float)System.Double.Parse(values[id + 1]);
        }

        public Vector2 GetVector2(int id)
        {
            if (values.Length <= (id + 1 + 1))
                return new Vector2(0.0f, 0.0f);

            return new Vector2(GetFloat(id), GetFloat(id + 1));
        }

        public Vector3 GetVector3(int id)
        {
            if (values.Length <= (id + 1 + 2))
                return new Vector3(0.0f, 0.0f, 0.0f);

            return new Vector3(GetFloat(id), GetFloat(id + 1), GetFloat(id + 2));
        }

        public Vector4 GetVector4(int id)
        {
            if (values.Length <= (id + 1 + 3))
                return new Vector4(0.0f, 0.0f, 0.0f, 0.0f);

            return new Vector4(GetFloat(id), GetFloat(id + 1), GetFloat(id + 2), GetFloat(id + 3));
        }
    }

    public sealed class INIReaderHeader
    {
        public string nickname;
        public List<INIReaderParameter> parameters = new List<INIReaderParameter>();

        public INIReaderHeader(string n)
        {
            this.nickname = n;
        }

        public bool Check(string name)
        {
            return nickname.CompareTo(name) == 0;
        }
    }

    public class INIReader
    {
        StringReader _reader;

        public INIReader(string contents)
        {
            ParseContents(contents);
        }

        List<INIReaderHeader> _headers = new List<INIReaderHeader>();

        public List<INIReaderHeader> GetHeaders()
        {
            return _headers;
        }

        private void ParseContents(string contents)
        {
            _reader = new StringReader(contents);
            INIReaderHeader currentHeader = null;

            while (0 == 0)
            {
                string line = _reader.ReadLine();

                if (line == null)
                {
                    if (currentHeader != null)
                        _headers.Add(currentHeader);

                    return;
                }
                else if (line.Length == 0)
                    continue;
                else if (line.StartsWith("\n"))
                    continue;
                else if (line.StartsWith("//"))
                    continue;
                else if (line.StartsWith("["))
                {
                    if (currentHeader != null)
                        _headers.Add(currentHeader);

                    currentHeader = ParseHeader(line);
                }
                else if (System.Char.IsLetter(line[0]))
                {
                    if (currentHeader != null)
                    {
                        INIReaderParameter p = ParseParameter(line);

                        if (p != null)
                            currentHeader.parameters.Add(p);
                    }
                }
            }
        }

        char[] paramSplitChars = { ',', '=', ' ', '	' };

        private INIReaderParameter ParseParameter(string line)
        {
            string[] values = line.Split(paramSplitChars, System.StringSplitOptions.RemoveEmptyEntries);

            if (values.Length == 0)
                return null;

            return new INIReaderParameter(values);
        }

        char[] headerTrimChars = { ' ', '	', '[', ']', '\n' };

        private INIReaderHeader ParseHeader(string line)
        {
            string headerName = line.Trim(headerTrimChars);

            if (headerName == null)
                return null;

            return new INIReaderHeader(headerName);
        }
    }
}
