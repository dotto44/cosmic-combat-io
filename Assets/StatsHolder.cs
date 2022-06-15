using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Core;
using TMPro; 
public class StatsHolder : MonoBehaviour {
    public static readonly int numberOfCharacters = 6;

    public static int stardustAmt = 0;
    /* private static string[] charAnimNamesArray = { "Earthling", "Rover", "Venusian", "Juppernaut" };
     private static string[] earthlingAnimCost = { "Default", "Santa", "Golden" };
     private static string[] roverAnimCost = { "Default", "Tuxedo"};
     private static string[] venusianAnimCost = { "Default", "Sentry"};
     private static string[] juppernautAnimCost = { "Default" };
     private static Dictionary<string, string[]> charAnimNames =  new Dictionary<string, string[]>()
     {
         { "Earthling", earthlingAnimCost},
         { "Rover", roverAnimCost},
         { "Venusian", venusianAnimCost},
         { "Juppernaut", juppernautAnimCost}
     };*/
    [SerializeField] MoveStartdustText moveStardustText;
    [SerializeField] ErrorWindow errorMessage;

    [SerializeField] Animator realPlayerAnimator;
    [SerializeField] RectTransform realPlayer;

    [SerializeField] SetBarAmount heartBar;
    [SerializeField] SetBarAmount fuelBar;
    [SerializeField] SetBarAmount speedBar;
    [SerializeField] SetBarAmount damageBar;
    [SerializeField] SetBarAmount firerateBar;
    [SerializeField] SetBarAmount rangeBar;

    [SerializeField] RuntimeAnimatorController[] skinAnimatorsNorm = new RuntimeAnimatorController[4];
    [SerializeField] RuntimeAnimatorController[] skinAnimatorsRover = new RuntimeAnimatorController[4];
    [SerializeField] RuntimeAnimatorController[] skinAnimatorsVen = new RuntimeAnimatorController[4];
    [SerializeField] RuntimeAnimatorController[] skinAnimatorsJup = new RuntimeAnimatorController[4];
    [SerializeField] RuntimeAnimatorController[] skinAnimatorsNep = new RuntimeAnimatorController[4];
    [SerializeField] RuntimeAnimatorController[] skinAnimatorsPluto = new RuntimeAnimatorController[4];

    [SerializeField] Sprite[] skinImagesNorm = new Sprite[4];
    [SerializeField] Sprite[] skinImagesRover = new Sprite[4];
    [SerializeField] Sprite[] skinImagesVen = new Sprite[4];
    [SerializeField] Sprite[] skinImagesJup = new Sprite[4];
    [SerializeField] Sprite[] skinImagesNep = new Sprite[4];
    [SerializeField] Sprite[] skinImagesPluto = new Sprite[4];

    [SerializeField] Animator[] heads = new Animator[numberOfCharacters];


    [SerializeField] RuntimeAnimatorController[][] allSkins = new RuntimeAnimatorController[numberOfCharacters][];
    Sprite[][] allStillSkins = new Sprite[numberOfCharacters][];
    [SerializeField] SkinBox[] skinImages = new SkinBox[4];
    [SerializeField] Animator[] cardAnims = new Animator[4];

    private static int[][] skinBoxes = {
    new int[]{ 0, 1, 2, 3 },
    new int[]{ 0, 1, 2, 4 },
    new int[]{ 0, 2, 4, 4 },
    new int[]{ 0, 4, 4, 3 },
    new int[]{ 0, 4, 4, 4 },
    new int[]{ 0, 4, 4, 4 }
        };


    [SerializeField] Animator buyButton;
    [SerializeField] WindowManager windowManager;
    private bool buyButtonUp;

    [SerializeField] Text buyText;


    [SerializeField] Text gunText;
    [SerializeField] Image gun;
    [SerializeField] List<Sprite> guns = new List<Sprite> { };

    public static bool[] achievementsCompleted = { false, false, false, false, false, false, false, false, false, false, false, false, false };
    [SerializeField] AchievementCounter achievementCounter;
    [SerializeField] private bool hasAchievements = true;
     [SerializeField] Achievement[] achievements;
    List<string> gunNames = new List<string> { "RAY GUN", "SCRAPPER", "TRI-SHOT", "BAZOOKA", "TRIDENT", "PUNCH"};

    [SerializeField] Text specialAbilityText;
    private List<string> specialNames = new List<string> { "GAMMA RAY", "OVERDRIVE", "HOOT-HOOT", "BIG BOOM", "BUBBLE BOMB", "TOXIC GAS"};
    [SerializeField] Image specialImage;
    [SerializeField] Color starBlue;

   // [SerializeField] Text nameText;
    [SerializeField] Animator player;
    [SerializeField] RectTransform playerTransform;
    private string[] playerNames = { "EARTHLING", "ROVER", "VENUSIAN", "JUPPERNAUT", "NEPTUNIAN", "PLUTONIAN"};

    private static string[] personalNames = { "NORM", "MARTIN", "WILLOW", "JACK", "CORAL", "PETER"};
    private static string[][] descriptions = {
    new string[]{"\"THE EARTHLING\"", "\"THE GIFT GIVER\"", "\"LOOKING SPIFFY\"", "\"THE GOLDEN BOY\"" },
    new string[]{"\"THE ROVER\"", "\"THE DETECTIVE\"", "\"THE SCRAP-HEAP\""},
    new string[]{"\"THE VENUSIAN\"", "\"THE DEFENDER\""}, 
    new string[]{"\"THE JUPPERNAUT\"", "", "", "\"THE SWASHBUCKLER\""},
    new string[]{"\"THE NEPTUNIAN\""},
    new string[]{"\"THE PLUTONIAN\""},
    };
    [SerializeField] Color[] gunIconColors;

    [SerializeField] TMP_Text personalNameText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] Image charMenuHead;

   
      
    private readonly int[] widthValues = { 141, 117, 188, 317, 212, 288 };



    private int[] characterPrices = { 0, 0, 0, 750, 750, 750};

    private int[] charSizes = { 192, 160, 256, 432, 288, 392 };

    const int rarePrice = 200;
    const int epicPrice = 400;
    const int legendaryPrice = 800;

    private static int[][] skinPrices = {
    new int[]{rarePrice, epicPrice, legendaryPrice },
    new int[]{rarePrice, epicPrice},
    new int[]{epicPrice},
    new int[]{0, 0, legendaryPrice},
    new int[]{}
        };

    public static bool[] unlockedChars = { true, true, true, true, true, true };
    public static int characterSelected = 0;
    private int characterViewing = 0;

    private int maxCharacterValue = 3;
    private int minCharacterValue = 0;



    [SerializeField] Sprite[] defaultCostumes = new Sprite[5];
    [SerializeField] Sprite[] altCostumes = new Sprite[5];
    [SerializeField] Sprite[] alt2Costumes = new Sprite[5];

    public static int[] heartStat = { 100, 125, 80, 200, 150, 110};
    public static int[] fuelStat = { 100, 100, 100, 100, 100,100 };
    public static int[] speedStat = { 70, 60, 75, 65, 72, 80};
    public static int[] damageStat = { 5, 9, 4, 10, 5, 1};
    public static int[] firerateStat = { 8, 3, 6, 2, 6, 1 };
    public static int[] rangeStat = { 7, 1, 7, 10, 6, 1};

    private Color blurred = new Color(0.3f, 0.3f, 0.3f, 1);
    private Color normalColor = new Color(1, 1, 1, 1);

    [SerializeField] Text stardustLabel;

    [SerializeField] SquareSmoothMove squareScript;
    [SerializeField] GameObject headHolder;
    //[SerializeField] Image square;
    //[SerializeField] Button head1;
    //[SerializeField] Image head1Image;
  // Image head2Image;
   // Image head3Image;
    //[SerializeField] GameObject head2;
   //[SerializeField] GameObject head3;

    [SerializeField] Animator brokenBuyBtn; 

    public static int[] selectedSkinPerChar = { 0, 0, 0, 0, 0, 0};
    private static int[] pastSelectedSkinPerChar = { 0, 0, 0, 0, 0, 0};
    //public static int selectedSkinNorm;
    //public static int selectedSkinRover;
    //public static int selectedSkinVen;
    //public static int selectedSkinJuppernaut;

    //public static bool[] earthlingCostumes = { false, false, false };
   // public static bool[] roverCostumes = { false, false };
   // public static bool[] venCostumes = { false, false };
   // public static bool[] jupperCostumes = { false, false };

    public static bool[][] allCostumes = new bool[][]
    {
        new bool[]{ false, false, false},
        new bool[]{ false, false, false},
        new bool[]{ false, false },
        new bool[]{ false, false, false },
        new bool[]{ false, false },
        new bool[]{ false, false }
    };
    public static readonly string[][] purchasesShortcodes =
    { 
        new string[]{ "", "SANTA", "COOLGUY", "GOLDEN" },
        new string[]{ "", "TUXEDO", "RUST" },
        new string[]{ "", "ARMOR" },
        new string[]{ "", "", "", "PIRATE" },
        new string[]{ "" },
         new string[]{ "" }
    };

    public static bool[] currentCostumes = { false, false };
	public static int currentSelectedSkin;
    private int previousSelectedSkin = 0;
    [SerializeField] GameObject goldBought;
	[SerializeField] GameObject tuxedoBought;
    [SerializeField] GameObject santaBought;
	[SerializeField] GameObject armorBought;

	[SerializeField] Button armorProfile;
	[SerializeField] Button goldProfile;
	[SerializeField] Button tuxedoProfile;
    [SerializeField] Button rustProfile;
    [SerializeField] Button santaProfile;
    [SerializeField] Button jupProfile0;
    [SerializeField] Button jupProfile1;

    [SerializeField] Sprite buySprite;
    [SerializeField] Sprite selectSprite;

    [SerializeField] TMP_Text nameInBottomLevelBar;
    [SerializeField] TMP_Text costumeInBottomLevelBar;
    [SerializeField] Image headInBottomLevelBarNew;
    public static float audioValue = 9;

    [SerializeField] CharacterLevelDisplay uiLevelController;
    [SerializeField] CharacterLevelDisplay charMenuLevelController;
    bool hasEnabled = false;

    void Start(){

        allSkins[0] = skinAnimatorsNorm;
        allSkins[1] = skinAnimatorsRover;
        allSkins[2] = skinAnimatorsVen;
        allSkins[3] = skinAnimatorsJup;
        allSkins[4] = skinAnimatorsNep;
        allSkins[5] = skinAnimatorsPluto;

        allStillSkins[0] = skinImagesNorm;
        allStillSkins[1] = skinImagesRover;
        allStillSkins[2] = skinImagesVen;
        allStillSkins[3] = skinImagesJup;
        allStillSkins[4] = skinImagesNep;
        allStillSkins[5] = skinImagesPluto;

    }
    public void WhenEnabld()
    {
       
        characterViewing = characterSelected;
        updateBarAmounts();
        updateOther();
        updateStardustText(false);

    }
    public void setBottomBarImages()
    {
        nameInBottomLevelBar.text = personalNames[characterSelected];
        costumeInBottomLevelBar.text = descriptions[characterSelected][currentSelectedSkin];
        updateHeadInBottomBar();
    }
    public void changeCharacterSelected(int to) {
        Debug.Log("CHANGED CHAR SELECTED TO: " + to);
        characterSelected = to;
        nameInBottomLevelBar.text = personalNames[to];
        costumeInBottomLevelBar.text = descriptions[characterSelected][currentSelectedSkin];
        updateHeadInBottomBar();
    }
    public void changeCostumeSelected(int to)
    {
        Debug.Log("changeCostumeSelected");
        currentSelectedSkin = to;
        costumeInBottomLevelBar.text = descriptions[characterSelected][to];
        updateHeadInBottomBar();
    }
    public void updateHeadInBottomBar()
    {
        headInBottomLevelBarNew.sprite = allStillSkins[characterSelected][currentSelectedSkin];
        //uiLevelController.updateLevelTextWith(characterSelected);
    }
    public static void setPastSelectedSkinsSame()
    {
        pastSelectedSkinPerChar[0] = selectedSkinPerChar[0];
        pastSelectedSkinPerChar[1] = selectedSkinPerChar[1];
        pastSelectedSkinPerChar[2] = selectedSkinPerChar[2];
        pastSelectedSkinPerChar[3] = selectedSkinPerChar[3];
    }
    public void pressedRight()
    {
        if (characterSelected < maxCharacterValue)
        {
            characterSelected++;
        }
        else
        {
            characterSelected = minCharacterValue;
        }
        Debug.Log("Pressed Right");
        Debug.Log("New Amount" + characterSelected);
        updateBarAmounts();
        updateOther();
    }
    public void pressedCharacter(int character)
    {
        characterViewing = character;
        updateBarAmounts();
        updateOther();
    }
    public void pressedLeft(){
		if (characterSelected > minCharacterValue) {
			characterSelected--;
		} else {
			characterSelected = maxCharacterValue;
		}

		Debug.Log ("Pressed Left");
		Debug.Log ("New Amount" + characterSelected);
		updateBarAmounts ();
		updateOther ();
	}
	public void updateBarAmounts(){
		heartBar.setBars (heartStat[characterViewing], unlockedChars [0] );
		fuelBar.setBars (fuelStat[characterViewing], unlockedChars [0] );
		speedBar.setBars (speedStat[characterViewing], unlockedChars [0] );
		//damageBar.setBars (damageStat[characterViewing], unlockedChars [0] );
		//firerateBar.setBars (firerateStat[characterViewing], unlockedChars [0] );
		//rangeBar.setBars (rangeStat[characterViewing], unlockedChars [0] );
	}
    public void exitCharMenuAndSaveData(bool isFromExitBtn)
    {

        
        setNotBoughtSkinsToDefault();
        int normSkin = selectedSkinPerChar[0];
        int venSkin = selectedSkinPerChar[2];
        int roverSkin = selectedSkinPerChar[1];
        int jupSkin = selectedSkinPerChar[3];
        if (isFromExitBtn)
        {
            currentSelectedSkin = previousSelectedSkin;
        }
        Debug.Log(currentSelectedSkin);
        bool shouldSave = false;

        if(pastSelectedSkinPerChar[0] != selectedSkinPerChar[0] 
        || pastSelectedSkinPerChar[1] != selectedSkinPerChar[1]
        || pastSelectedSkinPerChar[2] != selectedSkinPerChar[2]
        || pastSelectedSkinPerChar[3] != selectedSkinPerChar[3]
        || !isFromExitBtn)
        {
            shouldSave = true;
        }
        pastSelectedSkinPerChar[0] = selectedSkinPerChar[0];
        pastSelectedSkinPerChar[1] = selectedSkinPerChar[1];
        pastSelectedSkinPerChar[2] = selectedSkinPerChar[2];
        pastSelectedSkinPerChar[3] = selectedSkinPerChar[3];
        if (!shouldSave)
        {
            return;
        }
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("SAVE_CHARPREFS")
        .SetEventAttribute("Selected", characterSelected)
        .SetEventAttribute("NormSkin", normSkin)
        .SetEventAttribute("VenSkin", venSkin)
        .SetEventAttribute("RoverSkin", roverSkin)
        .SetEventAttribute("JupSkin", jupSkin)
        .Send((response) => {
            if (!response.HasErrors)
            {
                Debug.Log("CharMenu Saved To GameSparks...: ");
            }
            else
            {
                Debug.Log("Error Saving CharMenu Data..." + response.Errors);
                
            }
        });
    }
    private void flipCards()
    {
        int count = 0;
        foreach (Animator a in cardAnims)
        {
            if (a.GetInteger("rarity") == 0)
            {
                a.Play("BlueFlip", -1, 0);
                skinImages[count].SetRarity("Default");
            }
            else if (a.GetInteger("rarity") == 1)
            {
                a.Play("GreenFlip", -1, 0);
                skinImages[count].SetRarity("Rare");
            }
            else if (a.GetInteger("rarity") == 2)
            {
                a.Play("EpicFlip", -1, 0);
                skinImages[count].SetRarity("Epic");
            }
            else if (a.GetInteger("rarity") == 3)
            {
                a.Play("LegendaryFlip", -1, 0);
                skinImages[count].SetRarity("Legendary");
            }
            else if (a.GetInteger("rarity") == 4)
            {
                a.Play("NonExistinNormFlip", -1, 0);
                skinImages[count].SetRarity("N.A.");
            }
            a.SetInteger("rarity", skinBoxes[characterViewing][count]);
            skinImages[count].SetRarity(skinBoxes[characterViewing][count]);

            count++;
        }
    }

    private void updateOther(){
		for(int i = 0; i < skinImages.Length; i++)
        {
            skinImages[i].SetLiner(allSkins[characterViewing][i]);
            skinImages[i].SetSize(charSizes[characterViewing]);
        }
        flipCards();

        player.SetInteger ("playerNum", characterViewing);
        personalNameText.text = personalNames[characterViewing];

       //gun.sprite = guns [characterViewing];
        gun.color = gunIconColors[characterViewing];
		//nameText.text = playerNames [characterViewing];
		specialAbilityText.text = specialNames [characterViewing];
        heartBar.setText("" + heartStat[characterViewing]);
        fuelBar.setText("" + fuelStat[characterViewing]);
        speedBar.setText("" + speedStat[characterViewing]);
        gunText.text = gunNames [characterViewing];
		playerTransform.sizeDelta = new Vector2 (widthValues [characterViewing], widthValues [characterViewing]);
        Debug.Log("UpdateOther");
        previousSelectedSkin = currentSelectedSkin;
        currentSelectedSkin = selectedSkinPerChar[characterViewing];
	    currentCostumes = allCostumes[characterViewing];
        charMenuHead.sprite = allStillSkins[characterViewing][currentSelectedSkin];
        player.SetInteger ("costume", currentSelectedSkin);
        //charMenuLevelController.updateLevelTextWith(characterViewing);
        heads[characterViewing].SetInteger("num", currentSelectedSkin);
        heads[characterViewing].CrossFade("PlayerMenuClear", 0f);
      
        setBuyBasedOnSkin(allCostumes[characterViewing], currentSelectedSkin);
        updatePlayerAnimator();
        updateCharacterHeads();
    }
    private void updateAfterPurchase()
    {

    
        player.SetInteger("playerNum", characterViewing);
        personalNameText.text = personalNames[characterViewing];
        charMenuHead.sprite = allStillSkins[characterViewing][currentSelectedSkin];
       // charMenuLevelController.updateLevelTextWith(characterViewing);
        playerTransform.sizeDelta = new Vector2(widthValues[characterViewing], widthValues[characterViewing]);
        Debug.Log("updateAfterPurchase");
        currentSelectedSkin = selectedSkinPerChar[characterViewing];

        player.SetInteger("costume", currentSelectedSkin);
   
        //setBuyBasedOnSkin(allCostumes[characterViewing], currentSelectedSkin);
        updatePlayerAnimInstantly();


    }
    public void setSkin() {
        changeCharacterSelected(characterViewing);
        realPlayer.sizeDelta = new Vector2(widthValues[characterSelected], widthValues[characterSelected]);
        realPlayerAnimator.SetInteger("playerNum", characterSelected);
        realPlayerAnimator.SetInteger("costume", currentSelectedSkin);
        realPlayerAnimator.CrossFade("PlayerMenuClear", 0f);
    }
    public void updateFromSaveData()
    {
        changeCostumeSelected(selectedSkinPerChar[characterSelected]);
        realPlayer.sizeDelta = new Vector2(widthValues[characterSelected], widthValues[characterSelected]);
        realPlayerAnimator.SetInteger("playerNum", characterSelected);
        realPlayerAnimator.SetInteger("costume", currentSelectedSkin);
        realPlayerAnimator.CrossFade("PlayerMenuClear", 0f);
    }

    public void setNotBoughtSkinsToDefault()
    {
        int count = 0;
       
        foreach(int selection in selectedSkinPerChar)
        {
            if(selection != 0)
            {
                if (!allCostumes[count][selection - 1])
                {
                    selectedSkinPerChar[count] = 0;
                }
            }
            count++;
        }
    }
    public void updateCharacterHeads() {
        for (int a = 0; a < heads.Length; a++) {
            heads[a].SetInteger("num", selectedSkinPerChar[a]);
            heads[a].CrossFade("PlayerMenuClear", 0f);
        }

    }


    public static Color CombineColors(params Color[] aColors)
	{
		Color result = new Color(0,0,0,0);
		foreach(Color c in aColors)
		{
			result += c;
		}
		result /= aColors.Length;
		return result;
	}
	public void pressedBuyButton(){
		Debug.Log ("Pressd Buy");
        if (!GS.Authenticated) {
            errorMessage.sendErrorMessage("YOU NEED TO BE LOGGED IN TO PURCHASE ITEMS.");
            return;
         }
        if (unlockedChars[characterViewing])
        {
            buyAnyCostume(purchasesShortcodes[characterViewing][currentSelectedSkin], skinPrices[characterViewing][currentSelectedSkin - 1]);
        }
        else
        {
            if(currentSelectedSkin != 0)
            {
                errorMessage.sendErrorMessage("YOU NEED TO BUY THE CHARACTER IN ORDER TO PURCHASE SKINS.");
                return;
            }
            if (stardustAmt >= characterPrices[characterViewing])
            {
                buyCharacter(characterViewing);
            }
            else
            {
                errorMessage.sendErrorMessage("YOU DON'T HAVE ENOUGH MOON ROCKS.");
            }
        }
    }
	public void pressedDefaultCostume(){

        /*Vector3 tempSquare = square.transform.localPosition;
        tempSquare.x = head1.transform.localPosition.x;
        squareScript.moveToPos(tempSquare);*/   
        setCharacterCostume (0);
	}
	public void pressedAltCostume(){
      
		/*Vector3 tempSquare = square.transform.localPosition;
        tempSquare.x = head2.transform.localPosition.x;
        squareScript.moveToPos(tempSquare);*/
        setCharacterCostume (1);
	}
    public void pressedAlt2Costume()
    {

        /* Vector3 tempSquare = square.transform.localPosition;
         tempSquare.x = head3.transform.localPosition.x;
         squareScript.moveToPos(tempSquare);
         */
        setCharacterCostume(2);
    }
    public void pressedAlt3Costume(){
       
       /* Vector3 tempSquare = square.transform.localPosition;
        tempSquare.x = head3.transform.localPosition.x;
        squareScript.moveToPos(tempSquare);
        */       
        setCharacterCostume (3);
	}
	private void setCharacterCostume(int to){
        Debug.Log("setCharacterCostume");
        currentSelectedSkin = to;
      
		
        selectedSkinPerChar[characterViewing] = to;
        setBuyBasedOnSkin(allCostumes[characterViewing], to);

      
        player.SetInteger ("costume", to);
       
        heads[characterViewing].SetInteger("num", to);
        heads[characterViewing].CrossFade("PlayerMenuClear", 0f);
        updatePlayerAnimator();
    }
    public void pressedBuyOrSelect()
    {
        if (buyButton.GetBool("buy"))
        {
            pressedBuyButton();
        }
        else
        {
            setSkin();
            exitCharMenuAndSaveData(false);
            windowManager.turnOff();
        }
    }
    private void setBuyBasedOnSkin(bool[] boolArray, int costumeNum) {

        descriptionText.text = descriptions[characterViewing][costumeNum];
        charMenuHead.sprite = allStillSkins[characterViewing][costumeNum];
        //charMenuLevelController.updateLevelTextWith(characterViewing);
        if (costumeNum == 0 && !unlockedChars[characterViewing])
        {
            buyText.text = "" + characterPrices[characterViewing];
            buyButtonUp = true;
            buyButton.SetBool("buy", buyButtonUp);
          
            return;
        }
        if(costumeNum != 0 && !boolArray[costumeNum - 1])
        {
            buyText.text = "" + skinPrices[characterViewing][costumeNum - 1];
            buyButtonUp = true;
            buyButton.SetBool("buy", buyButtonUp);
            return;
        }
        buyButtonUp = false;
        buyButton.SetBool("buy", buyButtonUp);
    }

    public void updateStardustText(bool animated){
        if (!animated)
        {
            moveStardustText.updateLabel();
        }
        else
        {
            moveStardustText.animateLabelToCurrent();
        }


        //  stardustLabel.text = stardustAmt.ToString("D4");
    }
    
	public void buyCharacter(int characterIndex){
        Debug.Log(playerNames[characterIndex]);
        new GameSparks.Api.Requests.BuyVirtualGoodsRequest().SetCurrencyShortCode("STARDUST").SetQuantity(1).SetShortCode(playerNames[characterIndex]).Send((response) => {
		if (!response.HasErrors) {
			Debug.Log("Virtual Goods Bought Successfully...");
                buyButton.SetBool("breaking", true);
            confirmBought(characterIndex);
		} else {

                errorMessage.sendErrorMessage(ErrorMessages.whatsMyErrorMessage("PURCHASE", response.Errors));

            }
	});
	}
    public void buyAnyCostume(string shortCode, int price)
    {
        Debug.Log(shortCode);
        if (stardustAmt >= price)
        {
            new GameSparks.Api.Requests.BuyVirtualGoodsRequest().SetCurrencyShortCode("STARDUST").SetQuantity(1).SetShortCode(shortCode).Send((response) => {
                if (!response.HasErrors)
                {
                    Debug.Log("Virtual Goods Bought Successfully...");
                    confirmBoughtPrice(price, shortCode);
                    buyButton.SetBool("breaking", true);
                }
                else
                {
                   
                    Debug.Log("Error Buying Virtual Goods...");
                    errorMessage.sendErrorMessage(ErrorMessages.whatsMyErrorMessage("PURCHASE", response.Errors));


                }
            });
        }
        else
        {
            errorMessage.sendErrorMessage("YOU DON'T HAVE ENOUGH MOON ROCKS.");
        }
    }
    public void buyGolden(){
		if (stardustAmt >= legendaryPrice) {
			new GameSparks.Api.Requests.BuyVirtualGoodsRequest ().SetCurrencyShortCode ("STARDUST").SetQuantity (1).SetShortCode ("GOLDEN").Send ((response) => {
				if (!response.HasErrors) {
					Debug.Log ("Virtual Goods Bought Successfully...");
					confirmBoughtPrice (legendaryPrice, "Gold");

				} else {
					Debug.Log ("Error Buying Virtual Goods...");
				}
			});
		}
	}
	public void buySanta(){
		if (stardustAmt >= rarePrice) {
			new GameSparks.Api.Requests.BuyVirtualGoodsRequest ().SetCurrencyShortCode ("STARDUST").SetQuantity (1).SetShortCode ("SANTA").Send ((response) => {
				if (!response.HasErrors) {
					Debug.Log ("Virtual Goods Bought Successfully...");
					confirmBoughtPrice (rarePrice, "Santa");
				} else {
					Debug.Log ("Error Buying Virtual Goods...");
				}
			});
		}
	}
	public void buyTuxedo(){
		if (stardustAmt >= rarePrice) {
			new GameSparks.Api.Requests.BuyVirtualGoodsRequest ().SetCurrencyShortCode ("STARDUST").SetQuantity (1).SetShortCode ("TUXEDO").Send ((response) => {
				if (!response.HasErrors) {
					Debug.Log ("Virtual Goods Bought Successfully...");
					confirmBoughtPrice (rarePrice, "Tuxedo");
				} else {
					Debug.Log ("Error Buying Virtual Goods...");
				}
			});
		}
	}
    public void buyRust()
    {
        if (stardustAmt >= rarePrice)
        {
            new GameSparks.Api.Requests.BuyVirtualGoodsRequest().SetCurrencyShortCode("STARDUST").SetQuantity(1).SetShortCode("RUST").Send((response) => {
                if (!response.HasErrors)
                {
                    Debug.Log("Virtual Goods Bought Successfully...");
                    confirmBoughtPrice(epicPrice, "Rust");
                }
                else
                {
                    Debug.Log("Error Buying Virtual Goods...");
                }
            });
        }
    }
    public void buyArmor(){
		if (stardustAmt >= epicPrice) {
			new GameSparks.Api.Requests.BuyVirtualGoodsRequest ().SetCurrencyShortCode ("STARDUST").SetQuantity (1).SetShortCode ("ARMOR").Send ((response) => {
				if (!response.HasErrors) {
					Debug.Log ("Virtual Goods Bought Successfully...");
					confirmBoughtPrice (epicPrice, "Armor");
				} else {
					Debug.Log ("Error Buying Virtual Goods...");
				}
			});
		}
	}
	public void confirmBought(int characterIndex2){
		stardustAmt -= characterPrices[characterIndex2];
		unlockedChars[characterIndex2] = true;
        if(characterIndex2 == 3)
        {
            jupProfile0.interactable = true;
            jupProfile1.interactable = true;
            GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(4);
        }
        updateAfterPurchase();

        updateStardustText (true);
	}
	public void confirmBoughtPrice(int price, string Char){
		stardustAmt -= price;
		if (Char == "GOLDEN") {
            allCostumes[0][2] = true;
		//	goldBought.SetActive (true);
			goldProfile.interactable = true;
		}
        if (Char == "COOLGUY")
        {
            allCostumes[0][1] = true;
        }
        if (Char == "PIRATE")
        {
            allCostumes[3][2] = true;
        }
        if (Char == "TUXEDO") {
            allCostumes[1][0] = true;
			tuxedoProfile.interactable = true;
		}
        if (Char == "RUST")
        {
            allCostumes[1][1] = true;
            rustProfile.interactable = true;
        }
        if (Char == "ARMOR") {
            allCostumes[2][0] = true;
			//armorBought.SetActive (true);
			armorProfile.interactable = true;
		}
		if (Char == "SANTA") {
            allCostumes[0][0] = true;
			//santaBought.SetActive (true);
			santaProfile.interactable = true;
		}
        checkForCostumeAchievement();
        updateAfterPurchase();
        updateStardustText (true);
	}
    public void checkForCostumeAchievement()
    {
        int count = 0;
        foreach(bool myBool in allCostumes[0])
        {
            if (myBool)
                count++;
        }
        foreach (bool myBool in allCostumes[1])
        {
            if (myBool)
                count++;
        }
        foreach (bool myBool in allCostumes[2])
        {
            if (myBool)
                count++;
        }
        if(count == 1)
        {
            GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(5);
        }
        if (count == 3)
        {
            GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(6);
        }
    }
    public void unlockChar(int which){
		unlockedChars[which] = true;
        if(which == 3)
        {
            jupProfile0.interactable = true;
            jupProfile1.interactable = true;
        }
        //updateBarAmounts ();
        //updateOther ();
        //updateStardustText ();
    }
	public void unlockCostumeNorm(int which){
        Debug.Log("Which costume bro" + which);
        allCostumes[0][which] = true;
		if (which == 2) {
			goldBought.SetActive (true);
			goldProfile.interactable = true;
		}
		if (which == 0) {
			santaBought.SetActive (true);
			santaProfile.interactable = true;
		}
	}
	public void unlockCostumeRover(int which){

        allCostumes[1][which] = true;
		if (which == 0) {
			tuxedoBought.SetActive (true);
			tuxedoProfile.interactable = true;
		}
        if (which == 1)
        {
            rustProfile.interactable = true;
        }
    }
    public void unlockCostumeJupper(int which)
    {
        allCostumes[3][which] = true;
    }
    public void unlockCostumeVen(int which){
        allCostumes[2][which] = true;
		if (which == 0) {
			armorBought.SetActive (true);
			armorProfile.interactable = true;
		}
	//updateBarAmounts ();
		//updateOther ();
		//updateStardustText ();
	}
    public void setAchievement(int num)
    {
        achievementsCompleted[num] = true;
        if (hasAchievements)
        {
            achievements[num].uncover();
        }
      
    }
    public void setAchievementForCollect(int num)
    {
        achievementsCompleted[num] = true;
        if (hasAchievements)
        {
            achievements[num].readyToCollect();
        }

    }
    public void setAchievementNum(int num)
    {
        achievementCounter.setCount(num);
    }
    public void augmentAchievementNum()
    {
        achievementCounter.augmentAchievementNum();
    }
    public void resetAllShopComponents(){

		goldBought.SetActive (false);
       
	
        tuxedoBought.SetActive(false);
        armorBought.SetActive (false);
        rustProfile.interactable = false;
        tuxedoProfile.interactable = false;
        armorProfile.interactable = false;
		santaProfile.interactable = false;
		goldProfile.interactable = false;
        jupProfile0.interactable = false;
        jupProfile1.interactable = false;

        santaBought.SetActive (false);
		stardustLabel.text = "0000";
		//head2.SetActive (false);
	//head3.SetActive (false);
		stardustAmt = 0;
        changeCostumeSelected(0);
        changeCharacterSelected(0);
        realPlayerAnimator.SetInteger("playerNum", characterSelected);
        realPlayerAnimator.SetInteger("costume", currentSelectedSkin);
        realPlayerAnimator.CrossFade("PlayerMenuClear", 0f);
        for (int row = 0; row < allCostumes.Length; row++)
        {
            for (int col = 0; col < allCostumes[row].Length; col++)
            {
                allCostumes[row][col] = false;
            }
        }
        for (int i = 0; i <selectedSkinPerChar.Length; i++){
            selectedSkinPerChar[i] = 0;
        }
        for (int i = 0; i < achievementsCompleted.Length; i++)
        {
            achievementsCompleted[i] = false;
        }

        unlockedChars = new bool[]{true, true, true, false, false, false};


        if (hasAchievements)
        {
            foreach (Achievement ach in achievements)
            {
                ach.coverUp();
            }
        }
    }
    public void updatePlayerAnimator()
    {
      /*  string stateName = "";
        string key = charAnimNamesArray[characterSelected];
        if (unlockedChars[characterSelected]) {
            stateName = "" + key + " - " + charAnimNames[key][currentSelectedSkin];
        }
        else
        {
             stateName = "" + key + " - " + "Locked";
        }
        Debug.Log(stateName);*/
        player.CrossFade("PlayerMenuClear", 0f);

    }
    public void updatePlayerAnimInstantly()
    {
        player.CrossFade("PlayerMenuClearInstant", 0f);
    }
}
