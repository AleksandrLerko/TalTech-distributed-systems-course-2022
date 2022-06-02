using Base.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Base.Extensions;

public class CustomLangStrBinderProvider :  IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(LangStr))
        {
            return new LangStrBinderProvider();
        }

        return null;
    }
}