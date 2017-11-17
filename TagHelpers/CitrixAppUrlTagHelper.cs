using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

public class CitrixAppUrlTagHelper : TagHelper
{
    public CitrixApplicationInfo AppItem { get; set; }
    public string WindowTarget { get;set;}

    public override void Process (TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";

        if ( AppItem.ClientTypes.Contains("content") )
        {
            output.Attributes.Add("href",AppItem.Content);
        }
        else
        {
            output.Attributes.Add("href",$"Applications/Launch?AppID={AppItem.ID}");
        }

        output.Attributes.Add("target",WindowTarget);

    }
}