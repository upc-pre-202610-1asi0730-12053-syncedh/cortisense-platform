using Microsoft.AspNetCore.Mvc.ApplicationModels;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;

namespace SyncedHealth.Center.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;

/// <summary>
/// Represents the kebab case route naming convention in the CortiSense Platform.
/// </summary>
public class KebabCaseRouteNamingConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);

        foreach (var selector in controller.Actions.SelectMany(a => a.Selectors))
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);
    }

    private static AttributeRouteModel? ReplaceControllerTemplate(SelectorModel selector, string name)
    {
        return selector.AttributeRouteModel != null
            ? new AttributeRouteModel
            {
                Template = selector.AttributeRouteModel.Template?.Replace("[controller]", name.ToKebabCase())
            }
            : null;
    }
}