using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class map : MonoBehaviour
{
    public Button FastBattle, Battle;
    public Text info_power, enemypower, enemyLevel, gold_, board_, stone_, mineral_, cristal_, Res_Battle;
    public Text[]  LevelEn;
    public Button[] oldImg;
    public Image img;
    public Sprite[] IdieLeft, IdieRigth;
    private string[] powerPlayerEn;
    private int[] Power, EnAtt ,goldGift, boardGift, stoneGift, mineralGift, cristalGift;
    private int SwordmanAt = 20, ArcherAt = 25, CavarlyAt = 30, MagicAt = 35, PlayerPower, 
        SwordHealth = 15, ArcherHealth = 20, CavarlyHealth = 35, MagicHealth = 40, MinusPower = 0,
        swordman, archer, cavarly, magic, l=0, k1=0, k2=0, En=0;
    public static int gold, board, stone, mineral, cristal, levelEn, minusPower_;
    public static bool Win = true;
    private void Start()
    {
        Level_();
        Idie();
    }
    private void Level_()
    {
        Power = new[] {300, 1000, 3000, 4500, 6000, 8000, 10000, 15000};
        powerPlayerEn = new[] {"У вас нет армии для сражения с этим монстром!", "Бой безнадежен...", "Ноздря в ноздрю", "Пришел, увидел, победил."};
        goldGift = new[] {500, 1500, 2500, 3000, 3500, 4000, 4500, 5000};
        boardGift = new[] {6, 5, 8, 6, 7, 9, 7, 10};
        stoneGift = new[] {5, 6, 9, 8, 8, 8, 7, 10};
        mineralGift = new[] {0, 0, 1, 2, 1, 3, 4, 5};
        cristalGift = new[] {0, 0, 2, 1, 2, 4, 3, 5};
    }
    public void Info_about_Enemy(int idn)
    {
        IdieImg();
        levelEn = idn;
        if (maingame.curSwordman == 0 && maingame.curArcher==0 && maingame.curCavarly ==0 && maingame.curMagic ==0) {
            FastBattle.interactable = Battle.interactable = false;
            info_power.text = powerPlayerEn[0];
        }else FastBattle.interactable = Battle.interactable = true;
        PlayerPower = maingame.curSwordman * SwordmanAt + maingame.curArcher * ArcherAt +
                      maingame.curCavarly * CavarlyAt + maingame.curMagic * MagicAt;
        if (PlayerPower - Power[levelEn] > 1000) info_power.text = powerPlayerEn[3];
        else if (PlayerPower - Power[levelEn] < 1000 && PlayerPower - Power[levelEn] >= 0) info_power.text = powerPlayerEn[2];
        else if (PlayerPower - Power[levelEn] < 0 && PlayerPower>0) info_power.text = powerPlayerEn[1];
        enemyLevel.text = LevelEn[levelEn].text;
        enemypower.text = Power[levelEn].ToString();
        GetGift(levelEn);
    }
    private void GetGift(int idEn)
    {
        En = idEn;
        gold_.text = goldGift[idEn].ToString();
        board_.text = boardGift[idEn].ToString();
        stone_.text = stoneGift[idEn].ToString();
        mineral_.text = mineralGift[idEn].ToString();
        cristal_.text = cristalGift[idEn].ToString();
    }
    public void FastBatte()
    {
        if (Power[levelEn] - PlayerPower < 0)
        {
            Res_Battle.text = "Победа!\nНаграда получена!";
            ArmyDead();
            gold = goldGift[En];
            board = boardGift[En];
            stone = stoneGift[En];
            mineral = mineralGift[En];
            cristal = cristalGift[En];
        }
        else if (Power[levelEn] - PlayerPower > 0)
        {
            Res_Battle.text = "Поражение...\nНатренируйте воинов и попробуйте снова.";
            maingame.curSwordman = maingame.curArcher = maingame.curCavarly = maingame.curMagic = 0;
            gold = board = stone = mineral = cristal = 0;
        }
    }
    private void ArmyDead()
    {
        minusPower_ = MinusPower = (int) (Power[levelEn]*(float)(Random.Range(3f,10f)/10));
        PlayerPower -= MinusPower;
        if (maingame.curArcher==0 && maingame.curCavarly ==0 && maingame.curMagic ==0) maingame.curSwordman -= (int)Math.Round((float) (MinusPower/SwordHealth));
        else if (maingame.curSwordman == 0 && maingame.curCavarly == 0 && maingame.curMagic == 0) maingame.curArcher-= (int)Math.Round((float) (MinusPower/ArcherHealth));
        else if (maingame.curSwordman == 0 && maingame.curArcher == 0 && maingame.curMagic == 0) maingame.curCavarly-= (int)Math.Round((float) (MinusPower/CavarlyAt));
        else if (maingame.curSwordman == 0 && maingame.curArcher== 0 && maingame.curCavarly == 0) maingame.curMagic-= (int)Math.Round((float) (MinusPower/MagicHealth));
        else if (maingame.curSwordman == 0 && maingame.curArcher == 0)
        {
            do {cavarly = (int) Random.Range(1f, maingame.curCavarly);} while (CavarlyAt * cavarly > MinusPower) ;
            MinusPower -= cavarly * CavarlyAt;
            maingame.curCavarly -= cavarly;
            maingame.curMagic-= (int)Math.Round((float) (MinusPower/MagicHealth));
        }
        else if (maingame.curSwordman == 0 && maingame.curCavarly == 0)
        {
            do
            {
                archer = (int) Random.Range(1f, maingame.curArcher);
            } while (ArcherAt * archer > MinusPower);
            MinusPower -= archer * ArcherAt;
            maingame.curArcher-= archer;
            maingame.curMagic-= (int)Math.Round((float) (MinusPower/MagicHealth));
        }
        else if (maingame.curSwordman == 0 && maingame.curMagic == 0)
        {
            do
            {
                cavarly = (int) Random.Range(1f, maingame.curCavarly);
            } while (CavarlyAt * cavarly > MinusPower);
            MinusPower -= cavarly * CavarlyAt;
            maingame.curCavarly -= cavarly;
            maingame.curArcher-= (int)Math.Round((float) (MinusPower/ArcherHealth));
        }
        else if (maingame.curArcher == 0 && maingame.curCavarly == 0)
        {
            do
            {
                swordman = (int) Random.Range(1f, maingame.curSwordman);
            } while (SwordmanAt * swordman > MinusPower);
            MinusPower -= swordman * SwordmanAt;
            maingame.curSwordman -= swordman;
            maingame.curMagic-= (int)Math.Round((float) (MinusPower/MagicHealth));
        }
        else if (maingame.curArcher == 0 && maingame.curMagic == 0)
        {
            do
            {
                swordman = (int) Random.Range(1f, maingame.curSwordman);
            } while (SwordmanAt * swordman > MinusPower);
            MinusPower -= swordman * SwordmanAt;
            maingame.curSwordman -= swordman;
            maingame.curCavarly-= (int)Math.Round((float) (MinusPower/CavarlyHealth));
        }
        else if (maingame.curCavarly == 0 && maingame.curMagic == 0)
        {
            do
            {
                swordman = (int) Random.Range(1f, maingame.curSwordman);
            } while (SwordmanAt * swordman > MinusPower);
            Debug.Log("-swordman  " +swordman);
            MinusPower -= swordman * SwordmanAt;
            Debug.Log("-minuspower   " + MinusPower);
            maingame.curSwordman -= swordman;
            maingame.curArcher -= (int)Math.Round((float) (MinusPower/ArcherHealth));
            Debug.Log("swordman  " + maingame.curSwordman);
            Debug.Log("archer   " + maingame.curArcher);
        }
        else if (maingame.curSwordman == 0)
        {
            do {
                archer = (int) Random.Range(1f, maingame.curArcher);
            } while (ArcherAt * archer > MinusPower);
            MinusPower -= archer * ArcherAt;
            do {
                cavarly = (int) Random.Range(1f, maingame.curCavarly);
            } while (CavarlyAt * cavarly > MinusPower);
            MinusPower -= cavarly * CavarlyAt;
            maingame.curArcher -= archer;
            maingame.curCavarly -= cavarly;
            maingame.curMagic -= (int)Math.Round((float) (MinusPower/MagicHealth));
        }
        else if (maingame.curArcher == 0)
        {
            do {
                swordman = (int) Random.Range(1f, maingame.curSwordman);
            } while (SwordmanAt * swordman > MinusPower);
            MinusPower -= swordman * SwordmanAt;
            do {
                cavarly = (int) Random.Range(1f, maingame.curCavarly);
            } while (CavarlyAt * cavarly > MinusPower);
            MinusPower -= cavarly * CavarlyAt;
            maingame.curSwordman -= swordman;
            maingame.curCavarly -= cavarly;
            maingame.curMagic -= (int)Math.Round((float) (MinusPower/MagicHealth));
        }
        else if (maingame.curCavarly == 0)
        {
            do {
                swordman = (int) Random.Range(1f, maingame.curSwordman);
            } while (SwordmanAt * swordman > MinusPower);
            MinusPower -= swordman * SwordmanAt;
            do {
                archer = (int) Random.Range(1f, maingame.curArcher);
            } while (ArcherAt * archer > MinusPower);
            MinusPower -= archer * ArcherAt;
            maingame.curSwordman -= swordman;
            maingame.curArcher -= archer;
            maingame.curMagic -= (int)Math.Round((float) (MinusPower/MagicHealth));
        }
        else if (maingame.curMagic == 0)
        {
            do {
                swordman = (int) Random.Range(1f, maingame.curSwordman);
            } while (SwordmanAt * swordman > MinusPower);
            MinusPower -= swordman * SwordmanAt;
            do {
                archer = (int) Random.Range(1f, maingame.curArcher);
            } while (ArcherAt * archer > MinusPower);
            MinusPower -= archer * ArcherAt;
            maingame.curSwordman -= swordman;
            maingame.curArcher -= archer;
            maingame.curCavarly -= (int)Math.Round((float) (MinusPower/CavarlyHealth));
        }
        else
        {
            int temp = (int) Random.Range(1f, 4f);
            if (temp == 1) maingame.curSwordman -= (int)Math.Round((float) (MinusPower/SwordHealth));
            if (temp == 2) maingame.curArcher-= (int)Math.Round((float) (MinusPower/ArcherHealth));
            if (temp ==3) maingame.curCavarly-= (int)Math.Round((float) (MinusPower/CavarlyAt));
            if (temp == 4) maingame.curMagic -= (int)Math.Round((float) (MinusPower/MagicHealth));
        }
    }
    private void Idie()
    {
        oldImg[2].image.sprite =oldImg[3].image.sprite =oldImg[5].image.sprite = oldImg[7].image.sprite = IdieLeft[k1];
        oldImg[0].image.sprite =oldImg[1].image.sprite =oldImg[4].image.sprite = oldImg[6].image.sprite = IdieLeft[k1];
        k1++;
        if (k1 == 8)
            k1 = 0;
        Invoke("Idie", (float) 0.2);
    }
    private void IdieImg()
    {
        img.sprite = IdieRigth[l];
        l++;
        if (l == 8)
            l = 0;
        Invoke("IdieImg", (float) 0.2);
    }
}
