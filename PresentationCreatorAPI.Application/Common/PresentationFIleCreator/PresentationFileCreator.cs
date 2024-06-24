using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using PresentationEntity = PresentationCreatorAPI.Domain.Entites.Presentation;

namespace PresentationCreatorAPI.Application.Common.Helpers;

public class PresentationFileCreator
{
    public static async Task<string> CreatePresentation(PresentationEntity presentationEntity)
    {
        // Create a presentation document
        string root = "uploads/presentations/";
        var filePath = FileHelper.PresentationFilePathCreator(root);
        PresentationDocument presentationDocument = PresentationDocument.Create(filePath, PresentationDocumentType.Presentation);

        // Add a presentation part
        PresentationPart presentationPart = presentationDocument.AddPresentationPart();
        presentationPart.Presentation = new Presentation();

        // Add a slide part
        SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();
        Slide slide = new Slide(new CommonSlideData(new ShapeTree()));
        slidePart.Slide = slide;

        // Save the changes to the presentation part
        slidePart.Slide.Save();

        // Add slide part to presentation
        SlideIdList slideIdList = presentationPart.Presentation.AppendChild(new SlideIdList());
        SlideId slideId = slideIdList.AppendChild(new SlideId() { RelationshipId = presentationPart.GetIdOfPart(slidePart) });

        // Save the presentation document
        presentationPart.Presentation.Save();

        // Close the presentation document
        //presentationDocument.Close();
        return filePath;
    }
}
 