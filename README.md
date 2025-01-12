❌ **OBSOLETE! DO NOT USE.**  
✅ **USE:** [DGG Localization](https://github.com/DaniilDGG/DGG-Localization)

# Localization Unity Tools

Tools for managing localization in Unity.

This tool simplifies the process of adding localization support to Unity projects.

## Getting Started

1. **Set Up Languages**  
   Open the `Localization/Language Settings` window to configure the languages for your project.

2. **Add Localizations**  
   Use the `Localization/Localization Settings` window to create and manage localization items.  
   - Start by creating a unique identifier for each localization item.  
   - Provide translations for all configured languages.

3. **Use Localizations in Your Project**  
   - Add the `LocalizationInfo` component to manage localization data.  
   - For text localization, use the `TMPTextLocalization` component.  
     - Simply input the unique localization identifier for the desired text.

4. **Initialize the System**  
   Call `InitLocalizationSystem()` on the `LocalizationProfile` ScriptableObject to activate the localization system.  
   **Example**: Refer to the Demo Scene for implementation details.

5. **Switch Languages**  
   To change the current language, use:  
   ```csharp
   LocalizationController.SwitchLanguage(int languageIndex);
