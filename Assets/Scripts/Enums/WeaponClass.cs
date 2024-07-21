using System;

namespace Enums
{
    //These are the weapon classes, we need them for upgrade targeting and set bonuses
    
    //implemented as a binary sequence for multiple flag handling - can check each flag with bitwise operations
    //add multiple flags with binary OR (|)
    //check a state with binary AND (&) operation against what you're checking for: currentstate & CarState.Grounded == Carstate.Grounded
    //Remove a state with an inverted AND operation: currentstate = currentstate & ~Carstate.Grounded (or currentstate &= ~Carstate.Grounded)
    //Add a state with an OR operation: currentstate = currentstate | Carstate.Grounded (or currentstate |= Carstate.grounded)
    [Flags]
    public enum WeaponClass
    {
        // << means bitshifted left
        Blade       = 1 << 0,  // 000000000000001
        Blunt       = 1 << 1,  // 000000000000010
        Elemental   = 1 << 2,  // 000000000000100
        Ethereal    = 1 << 3,  // 000000000001000
        Explosive   = 1 << 4,  // 000000000010000
        Gun         = 1 << 5,  // 000000000100000
        Heavy       = 1 << 6,  // 000000001000000
        Legendary   = 1 << 7,  // 000000010000000
        Medical     = 1 << 8,  // 000000100000000
        Medieval    = 1 << 9,  // 000001000000000
        Precise     = 1 << 10, // 000010000000000
        Primitive   = 1 << 11, // 000100000000000
        Support     = 1 << 12, // 001000000000000
        Tool        = 1 << 13, // 010000000000000
        Unarmed     = 1 << 14  // 100000000000000
    }
}