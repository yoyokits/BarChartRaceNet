namespace BarChartRaceNet.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Markup;

    /// <summary>
    /// Defines the <see cref="EnumerationExtension" />.
    /// </summary>
    public class EnumerationExtension : MarkupExtension
    {
        #region Fields

        private Type _enumType;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerationExtension"/> class.
        /// </summary>
        /// <param name="enumType">The enumType<see cref="Type"/>.</param>
        public EnumerationExtension(Type enumType)
        {
            EnumType = enumType ?? throw new ArgumentNullException(nameof(enumType));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the EnumType.
        /// </summary>
        public Type EnumType
        {
            get => _enumType;
            private set
            {
                if (_enumType == value)
                {
                    return;
                }

                var enumType = Nullable.GetUnderlyingType(value) ?? value;
                if (enumType.IsEnum == false)
                {
                    throw new ArgumentException("Type must be an Enum.");
                }

                _enumType = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The ProvideValue.
        /// </summary>
        /// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var enumValues = Enum.GetValues(EnumType);

            return (
              from object enumValue in enumValues
              select new EnumerationMember
              {
                  Value = enumValue,
                  Description = GetDescription(enumValue)
              }).ToArray();
        }

        /// <summary>
        /// The GetDescription.
        /// </summary>
        /// <param name="enumValue">The enumValue<see cref="object"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string GetDescription(object enumValue)
        {
            var descriptionAttribute = EnumType
              .GetField(enumValue.ToString())
              .GetCustomAttributes(typeof(DescriptionAttribute), false)
              .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute != null
              ? descriptionAttribute.Description
              : enumValue.ToString();
        }

        #endregion Methods

        /// <summary>
        /// Defines the <see cref="EnumerationMember" />.
        /// </summary>
        public class EnumerationMember
        {
            #region Properties

            /// <summary>
            /// Gets or sets the Description.
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// Gets or sets the Value.
            /// </summary>
            public object Value { get; set; }

            #endregion Properties
        }
    }
}