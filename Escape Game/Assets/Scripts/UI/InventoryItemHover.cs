using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject inspectPanel; // Drag your inspect panel here in the inspector
    public Image inspectImage; // The Image component on the inspect panel
    public Sprite itemSprite; // The sprite of the item
    public TextMeshProUGUI Description; // Reference to the TMP component
    private string info;
    private void UpdateInfo()
    {
        switch (this.name)
        {
            case "Graphite_Mat":
                info = "Graphite";
                break;
            case "Lithium_Mat":
                info = "Lithium";
                break;
            case "Cobalt_Mat":
                info = "Cobalt";
                break;
            case "Nickel_Mat":
                info = "Nickel";
                break;
            case "Manganese_Mat":
                info = "Manganese";
                break;
            case "Lab_Report [N]":
                info = "**Lab Notes: Separator Development Trials**\n" +
                    "**Date:** 10/02/2035\n" +
                    "**Researcher:** Intern A. Lee\n\n" +

                    "**Objective:**\n" +
                    "Optimize the separator's ion flow efficiency while maintaining durability during extended cycles.\n\n" +

                    "**Tests Conducted:**\n" +
                    "- Experiment 1: Thick separators reduced wear but impeded ion flow.\n" +
                    "- Experiment 2: Thin separators enhanced ion flow efficiency by 10% but showed signs of rapid degradation.\n\n" +

                    "**Next Steps:**\n" +
                    "Further testing with medium-thickness separators is required to strike a balance between performance and longevity.\n\n" +

                    "**Unrelated Observation:**\n" +
                    "During a side project involving anode/cathode testing, researchers noted that some metallic anode materials tend\n" +
                    "to degrade faster without proper coating, while carbon-based materials performed consistently under stress.";
                break;
            case "Lab_Report (1) [R]":
                info = "**Internal Memo: Safety Protocols for Cathode Testing**\n" +
                    "**Date:** 15/03/2035\n" +
                    "**Author:** Safety Officer R. Clark\n\n" +

                    "**Summary:**\n" +
                    "Recent tests with manganese-heavy cathodes revealed recurring issues of thermal runaway. Despite its excellent\n" +
                    "availability and cost efficiency, manganese combinations tend to fail under rapid charge cycles.\n\n" +

                    "**Recommendations:**\n" +
                    "- Reduce manganese proportion in future cathode samples.\n" +
                    "- Conduct thermal chamber tests for all new samples.\n\n" +

                    "**Notable Observations:**\n" +
                    "One experiment utilizing cobalt-nickel blends showed improved stability, though it did not meet expected\n" +
                    "performance benchmarks. Further research is required.\n\n" +

                    "**Important Reminder:**\n" +
                    "Any future tests with lithium combinations must account for potential overheating when using cathodes with\n" +
                    "excess manganese.";
                break;
            case "Lab_Report (2) [A]":
                info  = "**Meeting Notes: Enhanced Anode Efficiency Project**\n" +
                    "**Date:** 22/04/2035\n" +
                    "**Attendees:** Dr. Amari, Dr. Kolovos, Research Interns\n\n" +

                    "**Discussion:**\n" +
                    "Today's discussion revolved around identifying the most effective materials for enhancing battery cycle life.\n" +
                    "We tested multiple combinations, but the pairing of a stable **carbon-based material** (graphite) for the anode and\n" +
                    "a **lightweight metallic element** (lithium) consistently delivered superior results. Dr. Amari emphasized the\n" +
                    "importance of selecting materials that ensure thermal stability during high-demand cycles.\n\n" +

                    "**Results:**\n" +
                    "- **Combination 1:** Manganese + Carbon: High efficiency, poor thermal stability.\n" +
                    "- **Combination 2:** Lithium + Graphite: Optimal cycle performance and stability.\n" +
                    "- **Combination 3:** Nickel + Copper: High safety but insufficient energy density.\n\n" +

                    "**Conclusion:**\n" +
                    "Graphite and lithium demonstrated unmatched compatibility for the anode's role. Further cathode combinations are\n" +
                    "being considered to maximize energy storage.";
                break;
            case "Lab_Report (3) [H]":
                info = "**Research Log: Thermal Analysis in High-Capacity Batteries**\n" +
                "**Date:** 12/05/2035\n" +
                "**Author:** Dr. E. Ramirez\n\n" +

                "**Objective:**\n" +
                "To determine the thermal behavior of anode-cathode combinations under rapid charging and discharging conditions.\n\n" +

                "**Findings:**\n" +
                "1. **Graphite as an Anode Material:**\n" +
                "   - Exhibited exceptional stability in high-temperature tests.\n" +
                "   - Carbon's resilience under thermal stress proved superior to alternatives.\n\n" +

                "2. **Lithium-Cobalt Pairing:**\n" +
                "   - Generated peak efficiency when paired with the optimized separator material (Ref: Separator v3).\n" +
                "   - Minimal degradation after 1,000 cycles at 70°C.\n\n" +

                "3. **Nickel-Copper Alternative:**\n" +
                "   - While offering moderate stability, this pairing failed to achieve competitive energy densities.\n\n" +

                "**Conclusion:**\n" +
                "The tests reaffirm that combinations involving **carbon-based anodes** and **lightweight metallic elements** are the frontrunners for our battery designs.\n" +
                "Further optimization of cathode compositions will focus on cobalt variations.\n\n" +

                "**Next Steps:**\n" +
                "- Isolate performance variances between Lithium-Cobalt and Lithium-Manganese combinations.\n" +
                "- Monitor degradation patterns during extended cycles.";

                break;
            case "Lab_Report (4) [R]":
                info = "**Internal Report: Alternative Cathode Testing**\n" +
                    "**Date:** 04/06/2035\n" +
                    "**Research Lead:** Dr. S. Patel\n\n" +

                    "**Summary:**\n" +
                    "The recent batch of cathodes utilizing manganese-heavy alloys displayed notable improvements in short-term energy output.\n" +
                    "However, repeated tests revealed severe thermal instability under prolonged stress conditions.\n" +
                    "These results are disappointing but not entirely unexpected.\n\n" +

                    "**Key Findings:**\n" +
                    "1. **Manganese-Dominant Compositions:**\n" +
                    "   - Improved energy density in the first 50 cycles.\n" +
                    "   - Rapid degradation after cycle 75.\n\n" +

                    "2. **Nickel Variations:**\n" +
                    "   - High thermal stability but insufficient energy density for practical use.\n\n" +

                    "3. **Lithium Pairings:**\n" +
                    "   - **Lithium-Manganese:** Exhibited dangerous thermal spikes under rapid discharge conditions.\n" +
                    "   - **Lithium-Cobalt:** Outstanding thermal control and cycle durability (limited samples tested).\n\n" +

                    "**Additional Notes:**\n" +
                    "Dr. Kolovos noted that further research into hybrid cobalt-nickel solutions might yield promising results for the cathode.\n" +
                    "However, current separator materials appear to amplify instability in manganese-heavy configurations.\n\n" +

                    "**Recommendations:**\n" +
                    "- Shift focus away from manganese-heavy cathodes due to safety concerns.\n" +
                    "- Prioritize cobalt-based solutions for future trials.";   
                break;
            case "Key":
                info = "The key to the chest containing the prototype !";
                break;
            default:
                info = "Unrecognized";
                break;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inspectPanel != null && inspectImage != null && Description != null)
        {
            inspectPanel.SetActive(true);
            inspectImage.sprite = itemSprite;
            UpdateInfo();
            Description.text = info;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inspectPanel != null)
        {
            inspectPanel.SetActive(false);
        }
    }
}
