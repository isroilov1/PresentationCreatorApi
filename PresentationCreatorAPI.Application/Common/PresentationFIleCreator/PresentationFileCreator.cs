using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using A = DocumentFormat.OpenXml.Drawing;
using P = DocumentFormat.OpenXml.Presentation;
using PresentationEntity = PresentationCreatorAPI.Domain.Entites.Presentation;

namespace PresentationCreatorAPI.Application.Common.Helpers;

public class PresentationFileCreator
{
    public static void CreatePresentation(PresentationEntity presentation)
    {
        using (PresentationDocument presentationDocument = PresentationDocument.Create(presentation.FilePath, PresentationDocumentType.Presentation))
        {
            PresentationPart presentationPart = presentationDocument.AddPresentationPart();
            presentationPart.Presentation = new Presentation();

            SlideIdList slideIdList = new SlideIdList();
            presentationPart.Presentation.Append(slideIdList);

            uint slideIdCounter = 1;

            string[] textLines = {
                "1. Public Perception of Government Services",
                "2. Consumer Satisfaction with Local Businesses",
                "3. Community Engagement in Local Issues",
                "4. Attitudes Towards Environmental Sustainability",
                "5. Public Opinion on Healthcare Reform",
                "6. Public Safety and Crime Prevention",
                "7. Transportation Needs and Preferences",
                "8. Access to Affordable Housing",
                "9. Education Quality and Accessibility",
                "10. Employment Opportunities and Job Satisfaction",
                "11. Cultural Diversity and Inclusion",
                "12. Social Media Use and Impact",
                "13. Health and Wellness Practices",
                "14. Financial Literacy and Planning",
                "15. Civic Engagement and Voter Participation",
                "16. Technology Adoption and Use",
                "17. Tourism and Leisure Activities",
                "18. Local Economic Development Initiatives"
            };

            foreach (var textLine in textLines)
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
                        new A.Paragraph(new A.Run(new A.Text("Iqtisodiyot"))))
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
                        new A.Paragraph(new A.Run(new A.Text(textLine))))
                };
                shapeTree.AppendChild(textShape);

                // Add image
                string imagePath = "templates/1/1.png";
                ImagePart imagePart = slidePart.AddImagePart(ImagePartType.Png);
                using (FileStream stream = new FileStream(imagePath, FileMode.Open))
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

