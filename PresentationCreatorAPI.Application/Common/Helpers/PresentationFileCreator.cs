using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using P = DocumentFormat.OpenXml.Presentation;
using A = DocumentFormat.OpenXml.Drawing;

namespace PresentationCreatorAPI.Application.Common.Helpers;

public class PresentationFileCreator
{
    public static void CreatePresentation(PresentationCreatorAPI.Entites.Presentation presentation)
    {
        using (PresentationDocument presentationDocument = PresentationDocument.Create(presentation.FilePath, PresentationDocumentType.Presentation))
        {
            PresentationPart presentationPart = presentationDocument.AddPresentationPart();
            presentationPart.Presentation = new Presentation();

            SlideIdList slideIdList = new SlideIdList();
            presentationPart.Presentation.Append(slideIdList);

            uint slideIdCounter = 1;

            foreach (var page in presentation.Pages)
            {
                SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();
                Slide slide = new Slide(new CommonSlideData(new ShapeTree()));

                var shapeTree = slide.CommonSlideData.ShapeTree;

                // Title shape
                var titleShape = new P.Shape()
                {
                    NonVisualShapeProperties = new P.NonVisualShapeProperties(
                        new P.NonVisualDrawingProperties() { Id = (UInt32Value)1U, Name = "Title 1" },
                        new P.NonVisualShapeDrawingProperties(),
                        new ApplicationNonVisualDrawingProperties()),
                    ShapeProperties = new P.ShapeProperties(),
                    TextBody = new P.TextBody(
                        new A.BodyProperties(),
                        new A.ListStyle(),
                        new A.Paragraph(new A.Run(new A.Text(page.Title))))
                };
                shapeTree.AppendChild(titleShape);

                // Text shape
                var textShape = new P.Shape()
                {
                    NonVisualShapeProperties = new P.NonVisualShapeProperties(
                        new P.NonVisualDrawingProperties() { Id = (UInt32Value)2U, Name = "Content 1" },
                        new P.NonVisualShapeDrawingProperties(),
                        new ApplicationNonVisualDrawingProperties()),
                    ShapeProperties = new P.ShapeProperties(),
                    TextBody = new P.TextBody(
                        new A.BodyProperties(),
                        new A.ListStyle(),
                        new A.Paragraph(new A.Run(new A.Text(page.Text))))
                };
                shapeTree.AppendChild(textShape);

                // Add image if path is not empty
                if (!string.IsNullOrEmpty(page.ImagesPath))
                {
                    ImagePart imagePart = slidePart.AddImagePart(ImagePartType.Png);
                    using (FileStream stream = new FileStream(page.ImagesPath, FileMode.Open))
                    {
                        imagePart.FeedData(stream);
                    }

                    var imagePartId = slidePart.GetIdOfPart(imagePart);

                    var picture = new P.Picture(
                        new P.NonVisualPictureProperties(
                            new P.NonVisualDrawingProperties { Id = (UInt32Value)3U, Name = "Picture 1" },
                            new P.NonVisualPictureDrawingProperties()),
                        new P.BlipFill(
                            new A.Blip { Embed = imagePartId },
                            new A.Stretch(
                                new A.FillRectangle())),
                        new P.ShapeProperties(
                            new A.Transform2D(
                                new A.Offset { X = 0, Y = 0 },
                                new A.Extents { Cx = 990000L, Cy = 792000L }),
                            new A.PresetGeometry(new A.AdjustValueList()) { Preset = A.ShapeTypeValues.Rectangle }));

                    shapeTree.AppendChild(picture);
                }

                slidePart.Slide = slide;

                SlideId slideId = new SlideId();
                slideId.RelationshipId = presentationPart.GetIdOfPart(slidePart);
                slideId.Id = slideIdCounter++;
                slideIdList.Append(slideId);
            }

            presentationPart.Presentation.Save();
        }
    }
}
