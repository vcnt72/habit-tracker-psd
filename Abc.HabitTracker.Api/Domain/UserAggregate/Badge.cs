using System;

namespace Abc.HabitTracker.Api.Domain
{
    public class Badge
    {
        private string _name;
        private string _description;


        public Badge(string name)
        {
            this._name = name;
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
        }

    }
}