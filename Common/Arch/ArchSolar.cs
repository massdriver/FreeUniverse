using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchSolar : ArchObject
    {
        public List<ArchSolarComponent> components { get; set; }

        public static readonly string ComponentList = "component_list";

        public ArchSolar()
        {
            components = new List<ArchSolarComponent>();
        }

        public override void ReadFromValueMap(ValueMap pmap)
        {
            base.ReadFromValueMap(pmap);

            components = ValueMap.ValueArrayToList<ArchSolarComponent>(pmap[ComponentList]);
        }

        public override ValueMap WriteToValueMap()
        {
            ValueMap pmap = base.WriteToValueMap();

            pmap[ComponentList] = ValueMap.ListToValueArray<ArchSolarComponent>(components);

            return pmap;
        }
    }
}
