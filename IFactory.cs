using System;

namespace Game
{
    interface IFactory
    {
        State create(String name);
    }
}