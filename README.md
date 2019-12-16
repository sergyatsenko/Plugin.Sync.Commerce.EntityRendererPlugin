# RenderEntityView Plugin to Render Any Sitecore Commerce Entity With MVC Razor View Engine



## Overview

What is the best way to retrieve and transform Sitecore Experience Commerce 9 (SXC9) data for use in an external system or even the Sitecore-based website? For example, generate an XML or JSON file with some specific information about a given product, or generate an email or a text message with order information, etc. Data integrations to get data to and from Commerce Engine is something we needed to do on pretty much every Sitecore Commerce project. Sitecore provides Commerce Service API and Sitecore Commerce Connect OOTB (Out off the box), which covers most common, but not all scenarios. To read data from extended Commerce Entities or to transform entity data into another format some custom code would need to be added to Commerce Engine APIs or on the data consumer side. Based on a number of such data integrations I created two plugins to help to get data to and from Sitecore Commerce Engine.

The plugin to transform and push data from an external system into SXC9 is called Commerce ImportPlugin and it's described in this blog post. The plugin to export SXC9 data is called RenderEntityView, it adds a couple of custom APIs to Commerce Engine read and transform data from any Commerce Entity into any kind string-based format (plain text, XML, JSON, HTML, etc.). RenderEntityView is using the MVC Razor engine to generate a view from a given template and using given entity object as a view model. Such generated view can be an HTML with product details, an email body with custom order details in it, Commerce Entities export in XML or JSON format, etc.

RenderEntityView plugin source code is [available on github](https://github.com/sergyatsenko/Plugin.Sync.Commerce.RenderEntityView) 

## How RenderEntityView Plugin works

RenderEntityView Plugin adds RenderEntityView pipeline plus two custom APIs, which take [Razor template name] and [entity ID] as parameters and returns rendered view as a string. The below diagram shows requests are processed by pipeline blocks in RenderEntityView pipeline.

![Render Entity View Flow Diagram](https://cdn.xcentium.com/-/media/images/blog-images/render-entity-view-plugin/renderentityview-plugin-flow-diagram.ashx?h=522&w=1300&la=en&hash=F7AF1E2B4066CD3851233CB1A3657FCC77138B00&vs=1&d=20191207T195635Z) 

# Razor Templates

### TEMPLATE FORMAT

Razor templates used by RenderEntityView plugin are no different from those used in modern ASP.NET MVC applications - same paradigm, same syntax, so anybody with experience in ASP.NET MVC should be comfortable with these. \@model directive must be present and specify the type of Commerce Entity used.

 

#### *Razor Templates examples:*

***@\*Render SellableItem properties in plain text\*@\***

\@model Sitecore.Commerce.Plugin.Catalog.SellableItem

Sellable Item fields in plain text.

ID: \@Model.Id

Name: \@Model.Name

DisplayName: \@Model.DisplayName

 

***@\*Render SellableItem properties to HTML\*@\***

\@model Sitecore.Commerce.Plugin.Catalog.SellableItem



# Sellable Item fields in HTML format





ID: \@Model.Id





Name: \@Model.Name





DisplayName: \@Model.DisplayName



 

***@\*Render any entity into JSON\*@\***

All Commerce Entities use Sitecore.Commerce.Core.CommerceEntity as the base class, so this model type will work for any entity

\@model Sitecore.Commerce.Core.CommerceEntity

\@Newtonsoft.Json.JsonConvert.SerializeObject(Model)

 

### RAZOR TEMPLATE LOCATIONS

Templates can be stored in the Commerce Engine file system or in the Sitecore content tree.

- If saving template in CE filesystem then make sure it's located directly or in a subfolder under [your Commerce Engine]\wwwroot\. In this case, [template path] format in API call should be a relative path to template file under [your Commerce Engine]\wwwroot\ (without square brackets)
- When a template is saved in Sitecore Content Tree then text or rich text field need to be used. In this case, [template path] format in API call should be as following: [item path]|[field name] (without square brackets)

 

## How to use RenderEntityView API

#### *Include RenderEntityView into your Commerce Engine solution*

Download [RenderEntityView project on githib](https://github.com/sergyatsenko/Plugin.Sync.Commerce.RenderEntityView) and add RenderEntityView plugin project to your Commerce Engine solution, make sure to reference RenderEntityView project from your engine project. There are no policies to configure. Deploy your Commerce solution (or run it from Visual Studio while testing)

#### *Supply one or more MVC Razor templates*

Create your Razor Template and save it as a file in Commerce Engine filesystem or as a field value in (any) Sitecore content items

#### *Call RenderEntityView API like so:*

- **To render a single entity**
  - {{ServiceHost}}/{{ShopsApi}}/RenderEntityView()
  - Parameters:
    - entityId: Commerce Entity ID
    - templatePath: path to template in Sitecore or CE's file system, e.g. Views/SellableItemView.cshtml for template file path or /sitecore/content/RazorTemplates/testRazorTemplate|SellableItemView for path to template in Sitecore
    - templateLocation: sitecore or file
- **To render multiple entities in one call using the same Razor template**
  - {{ServiceHost}}/{{ShopsApi}}/RenderEntityViews()
  - Parameters:
    - entityIds: pipe-delimeted list of Commerce Entity IDs, e.g. Entity-SellableItem-101|Entity-SellableItem-102
    - templatePath: path to template in Sitecore or CE's file system, e.g. Views/SellableItemView.cshtml for template file path or /sitecore/content/RazorTemplates/testRazorTemplate|SellableItemView for path to template in Sitecore
    - templateLocation: sitecore or file

 

Here's how these calls may look in Postman

![Render Entity View Postman Call 1](https://cdn.xcentium.com/-/media/images/blog-images/render-entity-view-plugin/render-entity-view-postman-call-1.ashx?la=en&hash=F892E0C6AFD5AC7E27E82DAEFDACD93E26DAAB34&vs=1&d=20191207T195712Z)

![Render Entity View Postman Call 2](https://cdn.xcentium.com/-/media/images/blog-images/render-entity-view-plugin/render-entity-view-postman-call-2.ashx?la=en&hash=7A645CC49184A0BC95E1AE64535C63512CC9292B&vs=1&d=20191207T195741Z)
