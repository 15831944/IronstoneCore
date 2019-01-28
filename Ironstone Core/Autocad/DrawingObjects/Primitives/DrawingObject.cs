﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace Jpp.Ironstone.Core.Autocad
{
    public abstract class DrawingObject
    {
        [XmlIgnore]
        DBObject _activeObject;

        public long BaseObjectPtr { get; set; }

        [XmlIgnore]
        public ObjectId BaseObject
        {
            get
            {
                if (BaseObjectPtr == 0)
                {
                    GenerateBase();
                }

                Document acDoc = Application.DocumentManager.MdiActiveDocument;
                Database acCurDb = acDoc.Database;
                return acCurDb.GetObjectId(false, new Handle(BaseObjectPtr), 0);
            }
            set
            {
                BaseObjectPtr = value.Handle.Value;
                CreateActiveObject();
            }
        }

        public void CreateActiveObject()
        {
            Transaction acTrans = Application.DocumentManager.MdiActiveDocument.TransactionManager.TopTransaction;
            _activeObject = acTrans.GetObject(BaseObject, OpenMode.ForWrite);
            _activeObject.Erased += ActiveObject_Erased;
            _activeObject.Modified += ActiveObject_Modified;
        }

        private void ActiveObject_Modified(object sender, EventArgs e)
        {
            ObjectModified(sender, e);
            DirtyModified = true;
        }

        protected virtual void GenerateBase()
        {
            throw new NullReferenceException("No base object has been linked");
        }

        protected abstract void ObjectModified(object sender, EventArgs e);

        private void ActiveObject_Erased(object sender, ObjectErasedEventArgs e)
        {
            Erased = true;
            ObjectErased(sender, e);
            DirtyRemoved = true;
        }

        protected abstract void ObjectErased(object sender, ObjectErasedEventArgs e);

        public bool Erased { get; set; }

        [XmlIgnore]
        public abstract Point3d Location { get; set; }

        public abstract void Generate();

        [XmlIgnore]
        public abstract double Rotation { get; set; }

        public bool DirtyModified { get; set; }
        public bool DirtyAdded { get; set; }
        public bool DirtyRemoved { get; set; }
    }
}
