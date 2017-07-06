using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GTI.Modules.SecurityCenter.Data;

namespace GTI.Modules.SecurityCenter
{
   internal class ModuleTreeNode: TreeNode 
    {
        internal  bool isModified;
        internal  int featuresCounter = 0;
        //internal  ModuleFeaturesData mFeaturesData;
        internal  string ModuleID;

        public ModuleTreeNode(string label) :base (label)
        {
            isModified=false ;
            featuresCounter = 0;
            //internal  ModuleFeaturesData mFeaturesData;
            ModuleID = "" ;
        }
    }
}
