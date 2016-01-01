using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchSolarComponentPropertyEngine : ArchSolarComponentProperty
    {
        public float forceThrustForwardBackward { get; set; }
        public float forceThrustUpDown { get; set; }
        public float forceThrustLeftRight { get; set; }
        public float torqueYaw { get; set; }
        public float torquePitch { get; set; }
        public float torqueRoll { get; set; }
        public float heatGenerationThrust { get; set; }
        public float heatGenerationTorque { get; set; }
        public float maxHeatCapacity { get; set; }
        public float overheatMalfunctionChance { get; set; }
        public float constPowerUsage { get; set; }
        public float coolantEfficiency { get; set; }
        public float empResistance { get; set; }
        public float oneshotForwardThrust { get; set; }
        public float oneshotForwardThrustHeat { get; set; }
        public float frictionTorque { get; set; }
        public float frictionVelocity { get; set; }

        public ArchSolarComponentPropertyEngine()
        {

        }

        public override void ReadHeader(INIReaderHeader header)
        {
            foreach (INIReaderParameter p in header.parameters)
            {
                if (p.Check("sound"))
                {
                    //sound = p.GetString(0);
                    continue;
                }
                else if (p.Check("exhaust"))
                {
                    //_engineExhaustEffects.Add(new ArchEngineExhaustEffect(p.GetStrkey64(0), p.GetStrkey64(1)));
                    continue;
                }
                else if (p.Check("force_thrust_forward_backward"))
                {
                    forceThrustForwardBackward = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("force_thrust_left_right"))
                {
                    forceThrustLeftRight = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("force_thrust_up_down"))
                {
                    forceThrustUpDown = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("force_torque_yaw"))
                {
                    torqueYaw = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("force_torque_pitch"))
                {
                    torquePitch = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("force_torque_roll"))
                {
                    torqueRoll = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("friction_torque"))
                {
                    frictionTorque = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("friction_velocity"))
                {
                    frictionVelocity = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("heat_generation_thrust"))
                {
                    heatGenerationThrust = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("heat_generation_torque"))
                {
                    heatGenerationTorque = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("max_heat_capacity"))
                {
                    maxHeatCapacity = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("overheat_malfunction_chance"))
                {
                    overheatMalfunctionChance = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("const_power_usage"))
                {
                    constPowerUsage = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("coolant_efficiency"))
                {
                    coolantEfficiency = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("emp_resistance"))
                {
                    empResistance = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("oneshot_forward_thrust"))
                {
                    oneshotForwardThrust = p.GetFloat(0);
                    continue;
                }
                else if (p.Check("oneshot_forward_thrust_heat"))
                {
                    oneshotForwardThrust = p.GetFloat(0);
                    continue;
                }
            }
        }
    }
}
