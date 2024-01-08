﻿using System;
using System.Web.WebPages;

namespace Web2023Project.Models
{
    public class Role
    {
        private int id;
        private string control;
        private string action;
        private int level;

        public Role(int id, string control, string action, int level)
        {
            this.id = id;
            this.control = control;
            this.action = action;
            this.level = level;
        }

        public Role(string control, string action)
        {
            this.control = control;
            this.action = action;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Control
        {
            get => control;
            set => control = value;
        }

        public string Action
        {
            get => action;
            set => action = value;
        }

        public int Level
        {
            get => level;
            set => level = value;
        }

        public bool containsInRole(string actionName, string controllerName)
        {
            if (this.action.ToLower().Equals(actionName.ToLower()) &&
                this.control.ToLower().Equals(controllerName.ToLower()) || this.action.IsEmpty() || this.control.IsEmpty())
            {
                return true;
            }
            return false;
        }
    }
}