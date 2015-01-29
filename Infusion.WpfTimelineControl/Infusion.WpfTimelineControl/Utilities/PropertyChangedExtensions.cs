using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Caliburn.Micro;
using Action = System.Action;

namespace Infusion.WpfTimelineControl.Utilities
{
    public static class PropertyChangedExtensions
    {
        /// <summary>
        /// A helper to attach a handler to PropertyChanged event using lambda expression.
        /// Note: this approach does not provide a way to unsubscribe from the event.
        /// </summary>
        public static void AttachToPropertyChanged<TProperty>(
            this INotifyPropertyChanged propertyChanged,
            Expression<Func<TProperty>> property,
            Action handler)
        {
            if (propertyChanged == null)
            {
                throw new ArgumentNullException("propertyChanged");
            }
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }

            propertyChanged.PropertyChanged += (sender, args) =>
            {
                var memberInfo = property.GetMemberInfo();
                if (args.PropertyName == memberInfo.Name)
                {
                    handler();
                }
            };
        }
    }
}
