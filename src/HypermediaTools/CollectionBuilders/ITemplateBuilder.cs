using HypermediaTools.Models;

namespace HypermediaTools.CollectionBuilders
{
    public interface ITemplateBuilder<TResource> where TResource:IAmAResource
    {
        Template BuildTemplate();
    }

    public class TemplateBuilder<TResource> : ITemplateBuilder<TResource> where TResource : IAmAResource
    {
        IFormatAsDataItem<TResource> data_item_formatter;

        public TemplateBuilder(IFormatAsDataItem<TResource> dataItemFormatter)
        {
            data_item_formatter = dataItemFormatter;
        }

        public Template BuildTemplate()
        {

            return new Template
                       {
                           data = data_item_formatter.FormatType(typeof (TResource))
                       };
        }
    }
}