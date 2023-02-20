using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class battle : MonoBehaviour
{
    public Text Num_Swordman, Num_Archer, Num_Cavarly, Num_Magic, Health_En, Max_Health;
    private int  temp = 0,index_ =0,  i = 0, j = 0, k=0, l = 0, num = 0, att_vrag = 0, gethit_vrag = 0, vrag_death = 0, healthMonstr,
        swordmanInt = 0, swordmanAttInt = 0, swordmanGet_Hit = 0, swordmanDeath_=0,
        archerIdie_ = 0, archerAtt_ = 0, archerGet_Hit = 0, archerDeath_=0,
        magicIdei_=0, magicAtt_=0, magicGet_Hit=0, magicDeath_=0,
        cavaleryIdei_=0, cavalryAtt_=0, cavaleryGet_Hit=0, cavaleryDeath_=0;
    private int[] Health, EnAtt, ArmyAt, ArmySost;
    public GameObject[] ArmyOld;
    private GameObject[] Tbattle, Armies;
    public Image Image_posl, Image_posl2;
    public Sprite[] Sprite_posl, vragIdie, vragAtt, vragGetHit, VragDeath,
        swordmanIdie, swordmanAtt, swordmanGetHit, swordmanDeath,
        archerIdie, archerAtt, archerGetHit, archerDeath, sprites,
        magicIdie, magicAtt, magicGetHit, magicDeath,
        cavaleryIdie, cavaleryAtt, cavaleryGetHit, cavaleryDeath;
    public Image vrag, swordman, archer, magic, cavalery, Result;
    public Text textResult;
    public Button Vrag;
    private bool vragIdiebool, click_to_att, vrag_att_bool, vrag_get_hit_bool, vrag_death_bool = false, deathvr = false,
        swordmanIdiebool = true, swordmanAttbool = false, swordmanGetHitbool = false, swordmanDeathbool = false, deathvrSwordman = false, 
        archerIdiebool = true, archerAttbool = false, archerGetHitbool = false, archerDeathbool = false, deathvrArcher = false,
        magicIdiebool = true, magicAttbool = false, magicGetHitbool = false, magicDeathbool = false, deathvrMagic = false,
        cavaleryIdiebool = true, cavaleryAttbool = false, cavaleryGetHitbool = false, cavaleryDeathbool = false, deathvrCavarly = false, att=false;
    private int[] goldGift, boardGift, stoneGift, mineralGift, cristalGift;
    void Start()
    {
        Num_Swordman.text = maingame.curSwordman.ToString();
        Num_Archer.text = maingame.curArcher.ToString();
        Num_Cavarly.text = maingame.curCavarly.ToString();
        Num_Magic.text = maingame.curMagic.ToString();
        Health = new[] {300, 800, 1500, 2500, 5000, 8000, 10000, 15000};
        EnAtt = new[] {5, 15, 25, 30, 45, 20, 85, 100};
        ArmyAt = new[] {20, 25, 30, 35};
        goldGift = new[] {500, 1500, 2500, 3000, 3500, 4000, 4500, 5000};
        boardGift = new[] {6, 5, 8, 6, 7, 9, 7, 10};
        stoneGift = new[] {5, 6, 9, 8, 8, 8, 7, 10};
        mineralGift = new[] {0, 0, 1, 2, 1, 3, 4, 5};
        cristalGift = new[] {0, 0, 2, 1, 2, 4, 3, 5};
        Max_Health.text = Health[map.levelEn].ToString() + "/";
        Health_En.text = Health[map.levelEn].ToString();
        healthMonstr = Health[map.levelEn];
        vragIdiebool = true;
        Status();
        Create_Armies();
        Vrag_Idie();
        Swordman_Idie();
        Archer_Idie();
        Cavalery_Idie();
        Magic_Idie();
        Image_posl.sprite =Sprite_posl[0];
        Image_posl2.sprite = Sprite_posl[1];
    }
    private void Update()
    {
        Invoke ("GameOver", 10);
    }
    private void GetGift(int idEn)
    {
        map.gold = goldGift[idEn];
        map.board = boardGift[idEn];
        map.stone = stoneGift[idEn];
        map.mineral = mineralGift[idEn];
        map.cristal = mineralGift[idEn];
    }
    private void Status()
    {
        if (maingame.curSwordman == 0)
        {
            GameObject.Find("swordman").SetActive(false);
            deathvrSwordman = true;
        }
        else k++;
        if (maingame.curArcher == 0)
        {
            GameObject.Find("archer").SetActive(false);
            deathvrArcher = true;
        }
        else k++;
        if (maingame.curCavarly == 0)
        {
            GameObject.Find("cavarly").SetActive(false);
            deathvrCavarly = true;
        }
        else k++;
        if (maingame.curMagic == 0)
        {
            GameObject.Find("magical").SetActive(false);
            deathvrMagic = true;
        }
        else k++;
        ArmySost = new int[k+1];
    }
    private void Create_Armies()
    {
        if (maingame.curSwordman != 0) {ArmySost[j] = 1; j++; }
        else Image_(ref Sprite_posl, j);
        if (maingame.curArcher != 0){ ArmySost[j] = 2; j++; }
        else Image_(ref Sprite_posl, j);
        if (maingame.curCavarly != 0) {ArmySost[j] = 3; j++; }
        else Image_(ref Sprite_posl, j);
        if (maingame.curMagic != 0){ ArmySost[j] = 4; j++; }
        else Image_(ref Sprite_posl, j);
        ArmySost[j] = 5;
        Debug.Log(Sprite_posl.Length);
    }
    private void GameOver()
    {
        if (deathvrSwordman == true && deathvrArcher== true && deathvrMagic == true && deathvrCavarly == true)
        {
            Image_posl.sprite = Image_posl2.sprite = Sprite_posl[0];
            Result.gameObject.SetActive(true);
            Vrag.interactable = false;
            textResult.text = "Поражение...\nНатренируйте вотнов и попробуйте снова.";
            maingame.curSwordman = maingame.curArcher = maingame.curCavarly = maingame.curMagic = 0;
        }
        if (deathvr == true)
        {
            if (Sprite_posl.Length >1)
            {
                Image_posl.sprite = Sprite_posl[num];
                Image_posl2.sprite = Sprite_posl[num + 1];
            }
            else if (Sprite_posl.Length == 1) {Image_posl.sprite = Image_posl2.sprite = Sprite_posl[0];}
            Result.gameObject.SetActive(true);
            Vrag.interactable = false;
            textResult.text = "Победа!\nНаграда получена!";
            map.gold = goldGift[map.levelEn]; map.board = boardGift[map.levelEn]; map.stone = stoneGift[map.levelEn]; 
            map.mineral = mineralGift[map.levelEn]; map.cristal = cristalGift[map.levelEn];
            maingame.curSwordman = Int32.Parse(Num_Swordman.text);
            maingame.curArcher = Int32.Parse(Num_Archer.text);
            maingame.curCavarly = Int32.Parse(Num_Cavarly.text);
            maingame.curMagic = Int32.Parse(Num_Magic.text);
        }
    }
    private void Image_(ref Sprite[] ar_, int index_)
    {
        if (ar_.Length-1 != index_)
        {
            for (int templ = index_; templ < ar_.Length - 1; templ++)
            {
                ar_[templ] = ar_[templ + 1];
            }
        }
        Array.Resize(ref ar_, ar_.Length-1);
    }
    private void MinusVoin(ref int[] array, ref Sprite[] ar, int index)
    {
        for (int templ = index; templ < array.Length-1; templ++)
        {
            array[templ] = array[templ + 1];
            ar[templ] = ar[templ + 1];
        }
        Array.Resize(ref array, array.Length-1);
        Array.Resize(ref ar, ar.Length-1);
    }
    private void Vrag_Idie()
    {
        if (vragIdiebool == true)
        {
            vrag.sprite = vragIdie[l];
            l++;
            if (l == 8)
                l = 0;
            Invoke("Vrag_Idie", (float) 0.1);
        }
        else l = 0;
    }
    private void Vrag_Att()
    {
       if (vrag_att_bool==true){ 
           vrag.sprite = vragAtt[att_vrag];
           if (att_vrag < 15)
            {
            Invoke("Vrag_Att", (float) 0.06);
            att_vrag++;
            }
            else 
           {
               vragIdiebool = true;
               click_to_att = false;
               att_vrag = 0;
               Vrag_Idie();
           }
       }
    }
    private void Get_Hit_vrag()
    {
        if (vrag_get_hit_bool==true){ 
            vrag.sprite = vragGetHit[gethit_vrag];
            if (gethit_vrag < 3)
            {
                Invoke("Get_Hit_vrag", (float) 0.15);
                gethit_vrag++;
            }
            else 
            {
                vrag_get_hit_bool = false;
                gethit_vrag = 0;
                if (healthMonstr <= 0) {
                    Health_En.text = 0.ToString();
                    Health_En.color = Color.red; 
                    vrag_death_bool = true;
                    Vrag_Death();
                }
                else
                {
                    vragIdiebool = true;
                    Vrag_Idie();
                    Health_En.text = healthMonstr.ToString();
                    Health_En.color = Color.black;
                }
            }
        }
    }
    private void Vrag_Death()
    {
        deathvr = true;
        if (vrag_death_bool == true)
        {
            vrag.sprite = VragDeath[vrag_death];
            if (vrag_death < 7)
            {
                Invoke("Vrag_Death", (float) 0.06);
                vrag_death++;
            }
            else {
                vrag_death_bool = false;
            }
        }
    }
    private void Attack_Vrag()
    {
        Image_posl.sprite =Sprite_posl[Sprite_posl.Length-1];
        Image_posl2.sprite = Sprite_posl[0];
        vragIdiebool = false;
        vrag_att_bool = true;
        Vrag_Att();
        temp = Random.Range(0, ArmySost.Length - 2);
        if (ArmySost[temp] == 1)
        {
            swordmanIdiebool = false;
            swordmanGetHitbool = true;
            Swordman_Get_Hit();
        }
        else if (ArmySost[temp] == 2)
        {
            archerIdiebool = false;
            archerGetHitbool = true;
            Archer_GetHit();
        }
        else if (ArmySost[temp]== 3)
        {
            cavaleryIdiebool = false;
            cavaleryGetHitbool = true;
            Cavarly_GetHit();
        }
        else if (ArmySost[temp] == 4)
        {
            magicIdiebool = false;
            magicGetHitbool = true;
            Magic_GetHit();
        }
        index_ = temp;
    }
    private void Swordman_Idie()
    {
        if (swordmanIdiebool == true)
        {
            swordman.sprite = swordmanIdie[swordmanInt];
            swordmanInt++;
            if (swordmanInt == 2)
                swordmanInt = 0;
            Invoke("Swordman_Idie", (float) 0.4);
        }
        else swordmanInt = 0;
    }
    private void Swordman_Att()
    {

        if (swordmanAttbool==true){ 
            swordman.sprite = swordmanAtt[swordmanAttInt];
            if (swordmanAttInt< 6)
            { 
                Invoke("Swordman_Att", (float) 0.06);
                swordmanAttInt++;
            }
            else 
            {
                swordmanIdiebool = true;
                swordmanAttbool = false;
                swordmanAttInt = 0;
                Swordman_Idie();
            }
        }
    }
    private void Swordman_Get_Hit ()
    {
        if (swordmanGetHitbool==true){ 
            swordman.sprite = swordmanGetHit[swordmanGet_Hit];
            if (swordmanGet_Hit < 2)
            {
                Invoke("Swordman_Get_Hit", (float) 0.15);
                swordmanGet_Hit++;
            }
            else 
            {
                swordmanGetHitbool = false;
                swordmanGet_Hit = 0;
                if ((int)((ArmyAt[0]*Int32.Parse(Num_Swordman.text)-EnAtt[map.levelEn])/ArmyAt[0]) <= 0) {
                    MinusVoin(ref ArmySost, ref Sprite_posl, index_);
                    Num_Swordman.text = 0.ToString();
                    swordmanDeathbool = true;
                    Swordman_Death();
                }
                else
                {
                    Num_Swordman.text = ((int)((ArmyAt[0]*Int32.Parse(Num_Swordman.text)-EnAtt[map.levelEn])/ArmyAt[0])).ToString();
                    swordmanIdiebool= true;
                    Swordman_Idie();
                }
            }
        }
    }
    private void Swordman_Death()
    {
        deathvrSwordman = true;
        if (swordmanDeathbool == true)
        {
            swordman.sprite = swordmanDeath[swordmanDeath_];
            if (swordmanDeath_ < 4)
            {
                Invoke("Swordman_Death", (float) 0.06);
                swordmanDeath_++;
            }
            else {
                swordmanDeathbool = false;
            }
        }
    }
    private void Archer_Idie()
    {
        if (archerIdiebool == true)
        {
            archer.sprite = archerIdie[archerIdie_];
            archerIdie_++;
            if (archerIdie_ == 11)
                archerIdie_ = 0;
            Invoke("Archer_Idie", (float) 0.1);
        }
        else archerIdie_ = 0;
    }
    private void Archer_Att()
    {
        if (archerAttbool==true){
            archer.sprite = archerAtt[archerAtt_];
            if (archerAtt_< 14)
            { 
                Invoke("Archer_Att", (float) 0.06);
                archerAtt_++;
            }
            else 
            {
                archerIdiebool = true;
                archerAttbool = false;
                archerAtt_ = 0;
                Archer_Idie();
            }
        }
    }
    private void Archer_GetHit()
    {
        if (archerGetHitbool==true){ 
            archer.sprite = archerGetHit[archerGet_Hit];
            if (archerGet_Hit < 5)
            {
                Invoke("Archer_GetHit", (float) 0.15);
                archerGet_Hit++;
            }
            else 
            {
                archerGetHitbool = false;
                archerGet_Hit = 0;
                if ((int)((ArmyAt[0]*Int32.Parse(Num_Archer.text)-EnAtt[map.levelEn])/ArmyAt[0]) <= 0)  {
                    MinusVoin(ref ArmySost, ref Sprite_posl, index_);
                    Num_Archer.text = 0.ToString();
                    archerDeathbool = true;
                    Archer_Death();
                }
                else
                {
                    Num_Archer.text = ((int)((ArmyAt[0]*Int32.Parse(Num_Archer.text)-EnAtt[map.levelEn])/ArmyAt[0])).ToString();
                    archerIdiebool= true;
                    Archer_Idie();
                }
            }
        }
    }
    private void Archer_Death()
    {
        deathvrArcher = true;
        if (archerDeathbool == true)
        {
            archer.sprite = archerDeath[archerDeath_];
            if (archerDeath_ < 18)
            {
                Invoke("Archer_Death", (float) 0.06);
                archerDeath_++;
            }
            else {
                archerDeathbool= false;
            }
        }
    }
    private void Cavalery_Idie()
    {
        if (cavaleryIdiebool == true)
        {
            cavalery.sprite =  cavaleryIdie[cavaleryIdei_];
            cavaleryIdei_++;
            if (cavaleryIdei_ == 4)
                cavaleryIdei_ = 0;
            Invoke("Cavalery_Idie", (float) 0.3);
        }
        else cavaleryIdei_ = 0;
    }
    private void Cavalery_Att()
    {
        if (cavaleryAttbool==true){
            cavalery.sprite = cavaleryAtt[cavalryAtt_];
            if (cavalryAtt_< 9)
            { 
                Invoke("Cavalery_Att", (float) 0.06);
                cavalryAtt_++;
            }
            else 
            {
                cavaleryIdiebool = true;
                cavaleryAttbool = false;
                cavalryAtt_ = 0;
                Cavalery_Idie();
            }
        }
    } 
    private void Cavarly_GetHit()
    {
        if (cavaleryGetHitbool==true){ 
            cavalery.sprite = cavaleryGetHit[cavaleryGet_Hit];
            if (cavaleryGet_Hit < 15)
            {
                Invoke("Cavarly_GetHit", (float) 0.15);
                cavaleryGet_Hit++;
            }
            else 
            {
                cavaleryGetHitbool = false;
                cavaleryGet_Hit = 0;
                if ((int)((ArmyAt[0]*Int32.Parse(Num_Cavarly.text)-EnAtt[map.levelEn])/ArmyAt[0]) <= 0) {
                    MinusVoin(ref ArmySost, ref Sprite_posl, index_);
                    Num_Cavarly.text = 0.ToString();
                    cavaleryDeathbool = true;
                    Cavarly_Death();
                }
                else
                {
                    Num_Cavarly.text = ((int)((ArmyAt[0]*Int32.Parse(Num_Cavarly.text)-EnAtt[map.levelEn])/ArmyAt[0])).ToString();
                    cavaleryIdiebool= true;
                    Cavalery_Idie();
                }
            }
        }
    }
    private void Cavarly_Death()
    {
        deathvrCavarly = true;
        if (cavaleryDeathbool == true)
        {
            cavalery.sprite = cavaleryDeath[cavaleryDeath_];
            if (cavaleryDeath_ < 10)
            {
                Invoke("Cavarly_Death", (float) 0.06);
                cavaleryDeath_++;
            }
            else {
                cavaleryDeathbool= false;
            }
        }
    }
    private void Magic_Idie()
    {
        if (magicIdiebool == true)
        {
            magic.sprite = magicIdie[magicIdei_];
            magicIdei_++;
            if (magicIdei_ == 5)
                magicIdei_ = 0;
            Invoke("Magic_Idie", (float) 0.2);
        }
        else magicIdei_ = 0;
    }
    private void Magic_Att()
    {
        if (magicAttbool==true){
            magic.sprite = magicAtt[magicAtt_];
            if (magicAtt_< 7)
            { 
                Invoke("Magic_Att", (float) 0.06);
                magicAtt_++;
            }
            else 
            {
                magicIdiebool = true;
                magicAttbool = false;
                magicAtt_= 0;
                Magic_Idie();
            }
        }
    } 
    private void Magic_GetHit()
    {
        if (magicGetHitbool==true){ 
            magic.sprite = magicGetHit[magicGet_Hit];
            if (magicGet_Hit < 3)
            {
                Invoke("Magic_GetHit", (float) 0.15);
                magicGet_Hit++;
            }
            else 
            {
                magicGetHitbool = false;
                magicGet_Hit = 0;
                if ((int)((ArmyAt[0]*Int32.Parse(Num_Magic.text)-EnAtt[map.levelEn])/ArmyAt[0]) <= 0) {
                    MinusVoin(ref ArmySost, ref Sprite_posl, index_);
                    Num_Magic.text = 0.ToString();
                    magicDeathbool = true;
                    Magic_Death();
                }
                else
                {
                    Num_Magic.text = ((int)((ArmyAt[0]*Int32.Parse(Num_Magic.text)-EnAtt[map.levelEn])/ArmyAt[0])).ToString();
                    magicIdiebool= true;
                    Magic_Idie();
                }
            }
        }
    }
    private void Magic_Death()
    {
        deathvrMagic = true;
        if (magicDeathbool == true)
        {
            magic.sprite = magicDeath[magicDeath_];
            if (magicDeath_ < 6)
            {
                Invoke("Magic_Death", (float) 0.06);
                magicDeath_++;
            }
            else {
                magicDeathbool= false;
            }
        }
    }
    public void Attec_En()
    {
        if (deathvr == false)
        {
            if (num == 0)
            {
                Image_posl.sprite = Sprite_posl[num];
                Image_posl2.sprite = Sprite_posl[num + 1];
            }
            if (ArmySost[num] == 1 && deathvrSwordman == false)
            {
                healthMonstr -= ArmyAt[0];
                vrag_get_hit_bool = true;
                vragIdiebool = false;
                swordmanAttbool = true;
                swordmanIdiebool = false;
                Swordman_Att();
                Get_Hit_vrag();
                if (ArmySost[num +1] ==5 && healthMonstr >0)
                {
                    Invoke("Attack_Vrag", 0.8f);
                }
            }
            if (ArmySost[num] == 2 && deathvrArcher == false)
            {
                healthMonstr -= ArmyAt[1];
                vrag_get_hit_bool = true;
                vragIdiebool = false;
                archerIdiebool = false;
                archerAttbool = true;
                Archer_Att();
                Get_Hit_vrag();
                if (ArmySost[num + 1] == 5 && healthMonstr >0)
                {
                    Invoke("Attack_Vrag", 0.8f);
                }
            }
            if (ArmySost[num] == 3 && deathvrCavarly == false)
            {
                healthMonstr -= ArmyAt[2];
                vragIdiebool = false;
                vrag_get_hit_bool = true;
                cavaleryAttbool = true;
                cavaleryIdiebool = false;
                Get_Hit_vrag();
                Cavalery_Att();
                if (ArmySost[num + 1] == 5 && healthMonstr >0)
                {
                    Invoke("Attack_Vrag", 0.8f);
                }
            }
            if (ArmySost[num] == 4 && deathvrMagic == false)
            {
                healthMonstr -= ArmyAt[3];
                vrag_get_hit_bool = true;
                vragIdiebool = false;
                magicIdiebool = false;
                magicAttbool = true;
                Get_Hit_vrag();
                Magic_Att();
                if (ArmySost[num + 1] == 5 && healthMonstr >0)
                {
                    Invoke("Attack_Vrag", 0.8f);
                }
            }
            if (num < ArmySost.Length - 2) num++;
            else num = 0;
            if (num != 0)
            {
                Image_posl.sprite = Sprite_posl[num];
                Image_posl2.sprite = Sprite_posl[num + 1];
            }
            
        }
    }
}