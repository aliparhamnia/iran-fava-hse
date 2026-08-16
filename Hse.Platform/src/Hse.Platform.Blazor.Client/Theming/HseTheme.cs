using MudBlazor;

namespace Hse.Platform.Blazor.Client.Theming;

public static class HseTheme
{
    public static readonly MudTheme Current = Create();

    public static MudTheme Create()
    {
        var font = new[] { "Vazirmatn", "sans-serif" };

        return new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary = "#0F766E",
                PrimaryContrastText = "#FFFFFF",
                Secondary = "#475569",
                SecondaryContrastText = "#FFFFFF",
                Tertiary = "#0D9488",
                AppbarBackground = "#115E59",
                AppbarText = "#F0FDFA",
                Background = "#F4F7F6",
                BackgroundGray = "#E8EEEC",
                Surface = "#FFFFFF",
                DrawerBackground = "#FFFFFF",
                DrawerText = "#134E4A",
                DrawerIcon = "#0F766E",
                TextPrimary = "#134E4A",
                TextSecondary = "#64748B",
                ActionDefault = "#0F766E",
                TableHover = "#CCFBF1",
                TableStriped = "#F0FDFA",
                LinesDefault = "#D5E3DF",
                Divider = "#D5E3DF",
                Success = "#059669",
                Error = "#DC2626",
                Warning = "#D97706",
                Info = "#0E7490"
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily = font,
                    FontSize = "0.9375rem",
                    FontWeight = "400",
                    LineHeight = "1.7"
                },
                H4 = new H4Typography { FontFamily = font, FontWeight = "700" },
                H5 = new H5Typography { FontFamily = font, FontWeight = "700" },
                H6 = new H6Typography { FontFamily = font, FontWeight = "600" },
                Button = new ButtonTypography { FontFamily = font, FontWeight = "600", TextTransform = "none" }
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "10px",
                DrawerWidthLeft = "280px",
                DrawerWidthRight = "280px"
            }
        };
    }
}
