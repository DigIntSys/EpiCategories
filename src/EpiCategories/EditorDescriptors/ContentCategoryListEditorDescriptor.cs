﻿using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Shell;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Geta.EpiCategories.EditorDescriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(ContentCategoryList))]
    public class ContentCategoryListEditorDescriptor : EditorDescriptor
    {
        private readonly IEnumerable<IContentRepositoryDescriptor> _contentRepositoryDescriptors;

        public ContentCategoryListEditorDescriptor(IEnumerable<IContentRepositoryDescriptor> contentRepositoryDescriptors)
        {
            _contentRepositoryDescriptors = contentRepositoryDescriptors;
            AllowedTypes = new[] { typeof(CategoryData) };
            ClientEditingClass = "geta-epicategories/widget/CategorySelector";
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);

            var categoryRepositoryDescriptor = _contentRepositoryDescriptors.FirstOrDefault(x => x.Key == CategoryContentRepositoryDescriptor.RepositoryKey);

            if (categoryRepositoryDescriptor == null)
                return;

            metadata.EditorConfiguration["roots"] = categoryRepositoryDescriptor.Roots;
        }
    }
}