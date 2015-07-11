using FreeUniverse.Common.Arch;
using FreeUniverse.Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    public class WorldController : IBaseObject
    {
        public int layer { get; private set; }
        protected NetworkMessageHandler messageHandler { get; set; }
        protected Dictionary<int, Solar> solars { get; set; }
        protected int solarid { get; set; }

        public WorldController(int layer)
        {
            this.solarid = 0;
            this.layer = layer;
            this.messageHandler = new NetworkMessageHandler();
            this.solars = new Dictionary<int, Solar>();
        }

        public Solar GetSolar(int id)
        {
            Solar s = null;

            if (solars.TryGetValue(id, out s))
                return s;

            return null;
        }

        public virtual Solar CreateSolar(ArchObject obj)
        {
            Solar solar = new Solar(this, solarid++, obj);

            solars[solar.id] = solar;

            return solar;
        }

        public virtual void Update(float dt)
        {
            foreach (KeyValuePair<int, Solar> e in solars)
                e.Value.Update(dt);
        }

        public virtual void Init()
        {
            throw new NotImplementedException();
        }

        public virtual void Release()
        {
            throw new NotImplementedException();
        }
    }
}
