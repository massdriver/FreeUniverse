using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    // MH: it just holds info about target arch, will be resolved later
    public sealed class ArchSubComponentReference
    {
        public ulong arch { get; set; }
        public ulong hp { get; set; }
        public ArchObject archObject { get; set; }
    }

    public sealed class ArchSolarComponent : ArchObject
    {
        public enum ComponentType
        {
            Null,
            Root,
            Slave
        }

        public ulong universalPrice { get; set; }
        public ComponentType type { get; set; }
        public ArchComponentHardpoint connectionHardpoint { get; set; }
        public List<ArchComponentHardpoint> hardpoints { get; set; }
        public ulong requiredConnectionHardpoint { get; set; }
        public List<ArchSubComponentReference> subComponents { get; set; }
        public List<ArchSolarComponentProperty> properties { get; set; }

        public ArchSolarComponent()
        {
            requiredConnectionHardpoint = ArchComponentHardpoint.HP_TYPE_ANY;
            subComponents = new List<ArchSubComponentReference>();
            hardpoints = new List<ArchComponentHardpoint>();
            properties = new List<ArchSolarComponentProperty>();
        }

        public override void ReadHeader(INIReaderHeader header)
        {
            if (header.Check("SubComponents"))
            {
                foreach (INIReaderParameter p in header.parameters)
                {
                    ArchSubComponentReference comp = new ArchSubComponentReference();
                    comp.arch = p.GetStrkey64(0);
                    comp.hp = p.GetStrkey64(1);
                    subComponents.Add(comp);
                }
            }
            else if (header.Check("Component"))
            {
                base.ReadHeader(header);

                foreach (INIReaderParameter p in header.parameters)
                {
                    if (p.Check("required_hardpoint_type"))
                    {
                        requiredConnectionHardpoint = p.GetStrkey64(0);
                    }
                }
            }
            else if (header.Check("Hull"))
            {
                ReadHull(header);
            }
            else if (header.Check("Hardpoints"))
            {
                ReadHardpoints(header);
            }
            else if (header.Check("Engine"))
            {
                ReadEngine(header);
            }
            else if (header.Check("Weapon"))
            {
                //ReadWeapon(header);
            }
        }

        void ReadEngine(INIReaderHeader header)
        {
            ArchSolarComponentPropertyEngine engine = new ArchSolarComponentPropertyEngine();
            engine.ReadHeader(header);
            properties.Add(engine);
        }

        void ReadHull(INIReaderHeader header)
        {
            ArchSolarComponentPropertyRigidHull hull = new ArchSolarComponentPropertyRigidHull();
            hull.ReadHeader(header);
            properties.Add(hull);
        }

        void ReadHardpoints(INIReaderHeader header)
        {
            foreach (INIReaderParameter p in header.parameters)
            {
                if (p.Check("hardpoint"))
                {
                    ArchComponentHardpoint hp = new ArchComponentHardpoint();
                    hp.ReadParameter(p);
                    hardpoints.Add(hp);
                    continue;
                }
                else if (p.Check("connection_hardpoint"))
                {
                    connectionHardpoint = new ArchComponentHardpoint();
                    connectionHardpoint.ReadParameter(p);
                    continue;
                }
            }
        }

        public static ComponentType StringToWorldObjectComponentType(string str)
        {
            if (str.CompareTo("root") == 0)
                return ComponentType.Root;

            if (str.CompareTo("slave") == 0)
                return ComponentType.Slave;

            return ComponentType.Null;
        }
    }
}
