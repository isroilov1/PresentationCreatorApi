using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using A = DocumentFormat.OpenXml.Drawing;
using PresentationEntity = PresentationCreatorAPI.Domain.Entites.Presentation;

namespace PresentationCreatorAPI.Application.Common.Helpers;

public class PresentationFileCreator
{
    public static void CreatePresentation(PresentationEntity presentationEntity)
    {
        // Create a presentation document
        PresentationDocument presentationDocument = PresentationDocument.Create("uploads/presentations/1/pres.pptx", PresentationDocumentType.Presentation);

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
    }
}
 