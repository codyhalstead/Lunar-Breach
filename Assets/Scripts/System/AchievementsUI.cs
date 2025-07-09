using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI : MonoBehaviour
{

    public Image destroyerImage;
    public Slider destroyerBar;
    public Image dronesImage;
    public Slider dronesBar;
    public Image swordsmanImage;
    public Slider swordsmanBar;
    public Image infantrymanImage;
    public Slider infantryBar;
    public Image wheelieImage;
    public Slider wheelieBar;
    public Image cannonImage;
    public Slider cannonBar;

    public Image level1Image;
    public Slider level1Bar;
    public Image level2Image;
    public Slider level2Bar;
    public Image level3Image;
    public Slider level3Bar;

    private Color greyOutColor = new Color(0.3f, 0.3f, 0.3f, 1f);

    public void SetUp(GameData currentData)
    {
        int destroyerNum = currentData.destroyersKilled;
        int dronesNum = currentData.dronesKilled;
        int swordsmanNum = currentData.swordsmenKilled;
        int infantryNum = currentData.infantrymenKilled;
        int wheelieNum = currentData.wheeliesKilled;
        int cannonNum = currentData.cannonsKilled;

        int level1Num = 0;
        int level2Num = 0;
        int level3Num = 0;
        if (currentData.completedStages.Contains("Stage1"))
        {
            level1Num = 1;
        }
        if (currentData.completedStages.Contains("Stage2"))
        {
            level2Num = 1;
        }
        if (currentData.completedStages.Contains("Stage3"))
        {
            level3Num = 1;
        }

        destroyerBar.value = destroyerNum;
        if (destroyerNum < 100)
        {
            destroyerImage.color = greyOutColor;
        }

        dronesBar.value = dronesNum;
        if (dronesNum < 100)
        {
            dronesImage.color = greyOutColor;
        }

        swordsmanBar.value = swordsmanNum;
        if (swordsmanNum < 100)
        {
            swordsmanImage.color = greyOutColor;
        }

        infantryBar.value = infantryNum;
        if (infantryNum < 100)
        {
            infantrymanImage.color = greyOutColor;
        }

        wheelieBar.value = wheelieNum;
        if (wheelieNum < 100)
        {
            wheelieImage.color = greyOutColor;
        }

        cannonBar.value = cannonNum;
        if (cannonNum < 100)
        {
            cannonImage.color = greyOutColor;
        }

        level1Bar.value = level1Num;
        if (level1Num < 1)
        {
            level1Image.color = greyOutColor;
        }

        level2Bar.value = level2Num;
        if (level2Num < 1)
        {
            level2Image.color = greyOutColor;
        }

        level3Bar.value = level3Num;
        if (level3Num < 1)
        {
            level3Image.color = greyOutColor;
        }

    }
}
