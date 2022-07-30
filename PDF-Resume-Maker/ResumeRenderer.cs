using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using Topten.RichTextKit;


namespace PDF_Resume_Maker
{
    public class ResumeRenderer
    
    {
        static SKColor GRAY128 = new(128, 128, 128);
        static SKColor GRAY89 = new(89, 89, 89);
        static SKColor BLACK = new(0, 0, 0);
        static SKColor BLUE = new(29, 130, 76);

        static string MIDDLE_DOT_SEP = " • ";

        static int NORMAL_WEIGHT = 400;
        static int BOLD_WEIGHT = 700;
        static float NAME_SIZE = 35;
        static float HEADING_SIZE = 14;
        static float BODY_SIZE = 11;
        static float SECTION_TITLE_SIZE = 13;

        static string HEADING_FONT = "Georgia";
        static string BODY_FONT = "Calibri";

        private SKPictureRecorder? currentPageRecorder;
        private SKCanvas? currentPageCanvas;
        private SKPoint currentPaintPos = new(0, 0);

        private float paperWidth = 8.5F;
        private float paperHeight = 11F;
        private float paperMargin = 1.0F;

        private float contentWidth;
        private float contentHeight;
        private float contentLeft;
        private float contentTop;
        public List<SKPicture> RenderResume(Resume_Data data)
        {
            List<SKPicture> pages = new();

            contentWidth = (paperWidth - 2 * paperMargin).InchToPt();
            contentHeight = (paperHeight - 2 * paperMargin).InchToPt();
            contentLeft = paperMargin.InchToPt();
            contentTop = paperMargin.InchToPt();
            currentPaintPos = new SKPoint(contentLeft, contentTop);

            currentPageRecorder = new SKPictureRecorder();

            var cullRect = new SKRect(0, 0, paperWidth.InchToPt(), paperHeight.InchToPt());
            currentPageCanvas = currentPageRecorder.BeginRecording(cullRect);

            var rsName = new RichString()
                .Alignment(TextAlignment.Center)
                .FontFamily(HEADING_FONT)
                .FontSize(NAME_SIZE)
                .Add(data.FirstName.ToUpper(), textColor: GRAY89)
                .Add(" ")
                .Add(data.LastName.ToUpper(), fontWeight: BOLD_WEIGHT, textColor: BLACK);
            rsName.MaxWidth = contentWidth;
            rsName.Paint(currentPageCanvas, currentPaintPos);

            var smallSpacing = 2F;
            currentPaintPos.Offset(0, rsName.MeasuredHeight + smallSpacing);

            var rsAddrPhone = new RichString()
                .Alignment(TextAlignment.Center)
                .FontFamily(BODY_FONT)
                .FontSize(BODY_SIZE)
                .TextColor(GRAY89)
                .Add(data.Address)
                .Add(MIDDLE_DOT_SEP)
                .Add(data.PhoneNumber);
            rsAddrPhone.MaxWidth = contentWidth;
            rsAddrPhone.MaxLines = 1;
            rsAddrPhone.Paint(currentPageCanvas, currentPaintPos);

            currentPaintPos.Offset(0, rsAddrPhone.MeasuredHeight);

            var rsOtherInfo = new RichString()
                .Alignment(TextAlignment.Center)
                .FontFamily(BODY_FONT)
                .FontSize(BODY_SIZE)
                .FontWeight(BOLD_WEIGHT)
                .TextColor(BLUE)
                .Add(data.Email)
                .Add(MIDDLE_DOT_SEP)
                .Add(data.Profile1)
                .Add(MIDDLE_DOT_SEP)
                .Add(data.Profile2);
            rsOtherInfo.MaxWidth = contentWidth;
            rsOtherInfo.MaxLines = 1;
            rsOtherInfo.Paint(currentPageCanvas, currentPaintPos);

            currentPaintPos.Offset(0, rsOtherInfo.MeasuredHeight + 1F.CmToPt());

            var lineStart = new SKPoint(0, currentPaintPos.Y);
            var lineEnd = new SKPoint(paperWidth.InchToPt(), currentPaintPos.Y);
            currentPageCanvas.DrawLine(lineStart, lineEnd, new SKPaint { Color = BLACK });

            currentPaintPos.Offset(0, 1F.CmToPt());

            var rsCareerObj = new RichString()
                .Alignment(TextAlignment.Left)
                .FontFamily(BODY_FONT)
                .FontSize(BODY_SIZE)
                .TextColor(GRAY89)
                .Add(data.CareerObj);
            rsCareerObj.MaxWidth = contentWidth;
            rsCareerObj.Paint(currentPageCanvas, currentPaintPos);

            currentPaintPos.Offset(0, rsCareerObj.MeasuredHeight);

            renderSectionHeader("EXPERIENCE");
            var beforeY = currentPaintPos.Y;
            foreach (var exp in data.Experiences)
            {
                var dateLine = exp.DateFrom + " – " + exp.DateTo;
                renderSectionContent(dateLine, exp.JobTitle, exp.Company, exp.Description);
                currentPaintPos.Offset(0, 10);
            }
            currentPaintPos.Offset(0, -10);
            var afterY = currentPaintPos.Y;
            renderSectionLine(new SKPoint(currentPaintPos.X, beforeY), new SKPoint(currentPaintPos.X, afterY));

            renderSectionHeader("EDUCATION");
            beforeY = currentPaintPos.Y;
            foreach (var edu in data.Education)
            {
                renderSectionContent(edu.MonthYear, edu.DegreeTitle, edu.School, edu.Description);
                currentPaintPos.Offset(0, 10);
            }
            currentPaintPos.Offset(0, -10);
            afterY = currentPaintPos.Y;
            renderSectionLine(new SKPoint(currentPaintPos.X, beforeY), new SKPoint(currentPaintPos.X, afterY));

            renderSectionHeader("SKILLS");
            renderBulletList(data.Skills);

            renderSectionHeader("ACTIVITIES");
            var rsAct = new RichString()
                .Alignment(TextAlignment.Left)
                .FontFamily(BODY_FONT)
                .FontSize(BODY_SIZE)
                .TextColor(GRAY89)
                .Add(data.Activities);
            rsAct.MaxWidth = contentWidth;
            rsAct.Paint(currentPageCanvas, currentPaintPos);

            currentPaintPos.Offset(0, rsAct.MeasuredHeight);

            var page = currentPageRecorder.EndRecording();
            pages.Add(page);

            return pages;
        }

        private void renderSectionHeader(string header)
        {
            currentPaintPos.Offset(0, 20);

            var rsHeader = new RichString()
                .Alignment(TextAlignment.Left)
                .FontFamily(HEADING_FONT)
                .FontSize(HEADING_SIZE)
                .FontWeight(BOLD_WEIGHT)
                .TextColor(BLACK)
                .Add(header.ToUpper());
            rsHeader.MaxWidth = contentWidth;
            rsHeader.Paint(currentPageCanvas, currentPaintPos);

            currentPaintPos.Offset(0, rsHeader.MeasuredHeight + 10);
        }

        private void renderSectionContent(string dateLine, string title, string subtitle, string description)
        {
            var sectionXOffset = 1F.CmToPt();

            var rsSection = new RichString()
                .Alignment(TextAlignment.Left)
                .FontFamily(BODY_FONT)
                .FontSize(BODY_SIZE)
                .Add(dateLine.ToUpper(), fontWeight: BOLD_WEIGHT, textColor: GRAY89)
                .Paragraph()
                .FontSize(SECTION_TITLE_SIZE)
                .Add(title.ToUpper() + ", ", fontWeight: BOLD_WEIGHT, textColor: BLUE)
                .Add(subtitle.ToUpper(), fontWeight: NORMAL_WEIGHT, textColor: GRAY89)
                .Paragraph()
                .FontSize(BODY_SIZE)
                .Add(description, fontWeight: NORMAL_WEIGHT, textColor: GRAY89);
            rsSection.MaxWidth = contentWidth - sectionXOffset;
            var pos = new SKPoint(currentPaintPos.X + sectionXOffset, currentPaintPos.Y);
            rsSection.Paint(currentPageCanvas, pos);

            currentPaintPos.Offset(0, rsSection.MeasuredHeight);
        }

        private void renderSectionLine(SKPoint start, SKPoint end)
        {
            var paint = new SKPaint
            {
                Color = GRAY128,
                StrokeWidth = 3F,
                PathEffect = SKPathEffect.CreateDash(new[] { 3F, 3F }, 0F),
            };
            currentPageCanvas.DrawLine(start, end, paint);
        }

        private void renderBulletList(List<string> list)
        {
            var itemXOffset = 1F.CmToPt();
            var bulletRadius = 1.5F;
            var lastYOffset = 0F;

            foreach (var item in list)
            {
                var rsItem = new RichString()
                    .Alignment(TextAlignment.Left)
                    .FontFamily(BODY_FONT)
                    .FontSize(BODY_SIZE)
                    .TextColor(GRAY89)
                    .Add(item);
                rsItem.MaxWidth = contentWidth;
                var pos = new SKPoint(currentPaintPos.X + itemXOffset, currentPaintPos.Y - 6F);
                rsItem.Paint(currentPageCanvas, pos);

                currentPageCanvas.DrawCircle(currentPaintPos.X + bulletRadius + 6F, currentPaintPos.Y + bulletRadius, bulletRadius, new SKPaint { Color = BLUE });
                lastYOffset = rsItem.MeasuredHeight;
                currentPaintPos.Offset(0, rsItem.MeasuredHeight);
            }
            currentPaintPos.Offset(0, -lastYOffset);
        }
    }

    static class UnitExtensions
    {
        // A PDF point is 1/72 of an inch
        public static float InchToPt(this float inch) => inch * 72;
        public static float CmToPt(this float cm) => cm / 2.54F * 72;
    }
}

