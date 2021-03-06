﻿using System;
using System.Globalization;
using System.Xml.Linq;
using GSoft.Dynamite.Binding;

namespace GSoft.Dynamite.Fields
{
    /// <summary>
    /// Defines the field info structure.
    /// </summary>
    /// <typeparam name="T">ValueType associated to that particular Field type</typeparam>
    public abstract class FieldInfo<T> : BaseTypeInfo, IFieldInfo
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        protected FieldInfo()
        {
        }

        /// <summary>
        /// Initializes a new FieldInfo
        /// </summary>
        /// <param name="internalName">The internal name of the field</param>
        /// <param name="id">The field identifier</param>
        /// <param name="fieldTypeName">Name of the type of field (site column type)</param>
        /// <param name="displayNameResourceKey">Display name resource key</param>
        /// <param name="descriptionResourceKey">Description resource key</param>
        /// <param name="groupResourceKey">Content Group resource key</param>
        protected FieldInfo(string internalName, Guid id, string fieldTypeName, string displayNameResourceKey, string descriptionResourceKey, string groupResourceKey)
            : base(displayNameResourceKey, descriptionResourceKey, groupResourceKey)
        {
            if (string.IsNullOrEmpty(internalName))
            {
                throw new ArgumentNullException("internalName");
            }
            else if (id == null || id == Guid.Empty)
            {
                throw new ArgumentNullException("id");
            }
            else if (internalName.Length > 32)
            {
                throw new ArgumentOutOfRangeException("internalName", "SharePoint field internal name cannot have more than 32 characters");
            }

            this.InternalName = internalName;
            this.Id = id;
            this.Type = fieldTypeName;
        }

        /// <summary>
        /// Creates a new FieldInfo object from an existing field schema XML
        /// </summary>
        /// <param name="fieldSchemaXml">Field's XML definition</param>
        protected FieldInfo(XElement fieldSchemaXml)
        {
            if (fieldSchemaXml == null)
            {
                throw new ArgumentNullException("fieldSchemaXml");
            }

            if (!XmlHasAllBasicAttributes(fieldSchemaXml))
            {
                throw new ArgumentException("Attribute missing from field definitions: ID, Name or Type.", "fieldSchemaXml");
            }

            this.Id = new Guid(fieldSchemaXml.Attribute("ID").Value);
            this.InternalName = fieldSchemaXml.Attribute("Name").Value;
            this.Type = fieldSchemaXml.Attribute("Type").Value;

            if (fieldSchemaXml.Attribute("DisplayName") != null)
            {
                // TODO: maybe try to parse $Resource string here... maybe not?
                this.DisplayNameResourceKey = fieldSchemaXml.Attribute("DisplayName").Value;
            }

            if (fieldSchemaXml.Attribute("Description") != null)
            {
                // TODO: maybe try to parse $Resource string here... maybe not?
                this.DescriptionResourceKey = fieldSchemaXml.Attribute("Description").Value;
            }

            if (fieldSchemaXml.Attribute("Group") != null)
            {
                // TODO: maybe try to parse $Resource string here... maybe not?
                this.GroupResourceKey = fieldSchemaXml.Attribute("Group").Value;
            }

            if (fieldSchemaXml.Attribute("Required") != null)
            {
                this.Required = bool.Parse(fieldSchemaXml.Attribute("Required").Value) ? RequiredType.Required : RequiredType.NotRequired;
            }

            if (fieldSchemaXml.Attribute("EnforceUniqueValues") != null)
            {
                this.EnforceUniqueValues = bool.Parse(fieldSchemaXml.Attribute("EnforceUniqueValues").Value);
            }

            if (fieldSchemaXml.Attribute("Hidden") != null)
            {
                this.IsHidden = bool.Parse(fieldSchemaXml.Attribute("Hidden").Value);
            }

            if (fieldSchemaXml.Attribute("ShowInDisplayForm") != null)
            {
                this.IsHiddenInDisplayForm = !bool.Parse(fieldSchemaXml.Attribute("ShowInDisplayForm").Value);
            }

            if (fieldSchemaXml.Attribute("ShowInEditForm") != null)
            {
                this.IsHiddenInEditForm = !bool.Parse(fieldSchemaXml.Attribute("ShowInEditForm").Value);
            }

            if (fieldSchemaXml.Attribute("ShowInNewForm") != null)
            {
                this.IsHiddenInNewForm = !bool.Parse(fieldSchemaXml.Attribute("ShowInNewForm").Value);
            }

            if (fieldSchemaXml.Attribute("ShowInListSettings") != null)
            {
                this.IsHiddenInListSettings = !bool.Parse(fieldSchemaXml.Attribute("ShowInListSettings").Value);
            }

            if (fieldSchemaXml.Attribute("DefaultFormula") != null)
            {
                this.DefaultFormula = fieldSchemaXml.Attribute("DefaultFormula").Value;
            }
        }

        /// <summary>
        /// Unique identifier of the field
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// The internal name of the field
        /// </summary>
        public string InternalName { get; private set; }

        /// <summary>
        /// SharePoint Field Type name of the field
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Indicates if the field is required
        /// </summary>
        public RequiredType Required { get; set; }

        /// <summary>
        /// Indicates if the field must enforce unique values
        /// </summary>
        public bool EnforceUniqueValues { get; set; }

        /// <summary>
        /// Indicates if field should be hidden
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Indicates if field should be shown in the display form
        /// </summary>
        public bool IsHiddenInDisplayForm { get; set; }

        /// <summary>
        /// Indicates if field should be shown in the new form
        /// </summary>
        public bool IsHiddenInNewForm { get; set; }

        /// <summary>
        /// Indicates if field should be shown in the edit form
        /// </summary>
        public bool IsHiddenInEditForm { get; set; }

        /// <summary>
        /// Indicates if field should be shown in the list settings
        /// </summary>
        public bool IsHiddenInListSettings { get; set; }

        /// <summary>
        /// Default formula for the field
        /// </summary>
        public string DefaultFormula { get; set; }

        /// <summary>
        /// Returns the FieldInfo's associated ValueType.
        /// For example, a TextFieldInfo should return typeof(string)
        /// and a TaxonomyFieldInfo should return typeof(TaxonomyValue)
        /// </summary>
        public Type AssociatedValueType
        {
            get
            {
                return typeof(T);
            }
        }

        /// <summary>
        /// Default field value.
        /// </summary>
        public T DefaultValue { get; set; }

        /// <summary>
        /// The XML schema of the Note field
        /// </summary>
        /// <param name="baseFieldSchema">
        /// The basic field schema XML (Id, InternalName, DisplayName, etc.) on top of which 
        /// we want to add field type-specific attributes
        /// </param>
        /// <returns>The full field XML schema</returns>
        public abstract XElement Schema(XElement baseFieldSchema);

        private static bool XmlHasAllBasicAttributes(XElement fieldSchemaXml)
        {
            return fieldSchemaXml.Attribute("ID") != null
                || fieldSchemaXml.Attribute("Name") != null
                || fieldSchemaXml.Attribute("Type") != null;
        }
    }
}
