using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.ComponentModel;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class maingame : MonoBehaviour
{
    public static int curSwordman = 0, curArcher = 0, curCavarly = 0, curMagic = 0;
    private int  hp = 0, maxhp = 250, _goldNum , _stoneNum , _boardNum , _crystalNum , _mineralNum , _level, _power; 
    private int Swordman_num=0, Archer_num=0, Cavarly_num=0, Magic_num=0, num =1, Gold_num=0, Board_num=0, Stone_num=0, Mineral_num=0, Cristal_num=0, all_tasks=0;
    private int idn, res = 0, maxlevelBuild = 10, maxlevelCastle = 20, i = 0, maxLevelPlayer = 20, temp = 0, powerCastl = 150, 
            powerArmiBuild = 50, powerArmiBuild2 = 50, powerArmiBuild3 = 50, powerArmiBuild4 = 50, 
            powerRes = 35, powerRes2 = 35, powerRes3 = 35, powerRes4 = 35, powerRes5 = 35, powerRes6 = 35, idBuild=0;
    private int levelbuild = 1, gold_ = 1500, gold_2=500, stone_ = 10, board_ = 10, mineral_ = 4, cristal = 4, swordsman = 15, archer = 15, magician = 5, cavalry = 10;
    public int id;
    private float TimergoldCastle = 0, timergold = 0, timerBoard = 0, timerstone = 0, timercrystal = 0, timermineral = 0,
        timerSwordman = 0, timerArcher = 0, timerMagic = 0, timerCavalry = 0;
    public Text gold, stone, board, crystal, mineral , name,biographi, levelPlayer, Player_, powerPlayer, Power_,
        castlelevel, swordsmanlevel, archerlevel, magicallevel, cavalrylevel, marketlevel, 
        boardlevel,  stonelevel, minerallevel, crystallevel, goldlevel,
        NumSwordman, NumArcher, NumMagical, NumCavalry, 
        nameBuilding, inf, Gold_to_market;
    public Text[] quantity_of_gold, quantity_of_tree, quantity_of_crist, quantity_of_stone, quantity_of_mineral, description, name_of_bilding, info, 
        level_of_building, nextLevel , MarketRes, task_text, gift_res;
    public Image  old2, oldImage, oldImage2;
    public Image[] oldImg, old, task_cheak;
    public Sprite[] image_of_building, newSprite,  tasks_, newSprites;
    public Button buyButton, GetButton, map_Battle, Get_Gift, ImagePerson;
    public Button[] Buildings, Build_, next, maxlevelbut, more, less;
    private bool add = true, add1 = false, add2 = false, add3 = false, add4 = false, add5 = false, add6 = false, add7 = false, add8 = false, add9 = false, 
        buy = false, task1=false, task2=false, task3=false, click = false;
    private int _spriteNum; 
    public Text oldName, oldBiograpfi;
    private int[] res_, _gold_, _board_, _stone_, _mineral_, _cristal_;
    private string[] _newName, _newBiographi, nameBuild, descBuild;
    private Text[] level_Build;
    private string _namenew;
    private string path, nameFile;
    private int task1_, task2_, task3_,
        swordman_ =0, archer_ =0, cavarly_=0, magic_=0,
        market_=0, gold_m=0, board_m=0, stone_m=0, mineral_m=0, cristal_m=0;
    public static bool readfile = false;
    private static bool swordman_bool = false, archer_bool = false, cavarly_bool = false, magic_bool = false, market_bool = false, 
        gold_bool = false, board_bool = false, stone_bool = false, mineral_bool=false, cristal_bool = false;

    void Start()
    {
        path = Application.persistentDataPath; 
        nameFile = "save.txt";
        if (readfile == true) {ReadFromFile();}
        else if (!PlayerPrefs.HasKey("saved")) { HardLevel(); DescriptionA();}
        else { LoadData2(); PlayerPrefs.DeleteAll(); }
        StartGame();
        tasks();
        TimerGoldCastle();
    }
    private void StartGame()
    {
        gold.text = _goldNum.ToString();
        stone.text = _stoneNum.ToString();
        board.text = _boardNum.ToString();
        crystal.text = _crystalNum.ToString();
        mineral.text = _mineralNum.ToString();
        levelPlayer.text = _level.ToString();
        powerPlayer.text = _power.ToString();
        Player_.text = "уровень: " + levelPlayer.text;
        Power_.text = "мощь: " + powerPlayer.text;
        NumSwordman.text = Swordman_num.ToString() + " чел.";
        NumArcher.text = Archer_num.ToString() + " чел.";
        NumCavalry.text = Cavarly_num.ToString() + " чел.";
        NumMagical.text = Magic_num.ToString() + " чел.";
        ImagePerson.image.sprite = oldImage.sprite = oldImage2.sprite = newSprites[_spriteNum];
        gold_ += (Int32.Parse(castlelevel.text)-1)*500;
        swordsman += (Int32.Parse(swordsmanlevel.text)-1)*5;
        archer += (Int32.Parse(archerlevel.text)-1)*5;
        cavalry += (Int32.Parse(cavalrylevel.text)-1)*3;
        magician += (Int32.Parse(magicallevel.text)-1)*2;
        gold_2 += (Int32.Parse(goldlevel.text)-1)*100;
        board_ += (Int32.Parse(boardlevel.text)-1)*3;
        stone_ += (Int32.Parse(stonelevel.text)-1)*3;
        mineral_ += (Int32.Parse(minerallevel.text)-1)*1;
        cristal += (Int32.Parse(crystallevel.text)-1)*1;
        name.text = "имя: Морнэмир («Черный алмаз»)";
        biographi.text = "биография: Представитель одного из свободных народов Средиземья (эльфов), объединившихся для борьбы со Злом. Сын короля лунных эльфов, практически неуязвимый воин, заведший дружбу с Тамиорном.";
        nameBuild = new[] {"Замок", "Рыцари", "Лучники",  "Конница", "Маги", "Рынок","Золотая шахта", "Лесопилка", "Каменоломня", "Рудниковая шахта", "Кристалльная шахта" };
        descBuild = new[] { "золото: ", "рыцари: ", "лучники: ", "конница: ", "маги: ","Позволяет обменивать золото на ресурсы.", "золото: ", "дерево: ", "камень: ", "руда: ",  "кристаллы: "};
        res_ = new[] {gold_, swordsman, archer, cavalry, magician, gold_2, board_, stone_, mineral_, cristal };
        level_Build = new[] {castlelevel, swordsmanlevel, archerlevel, cavalrylevel, magicallevel, marketlevel, goldlevel, boardlevel, stonelevel, minerallevel, crystallevel };
        _gold_ = new[] {2500, 2000, 2500, 3000, 3500, 2000, 2000, 2000, 4000, 4000, 4500};
        _board_ = new[] {15, 10, 15, 15, 15, 10, 20, 10, 15, 20, 20};
        _stone_ = new[] {5, 15, 10, 20, 15, 10, 10, 20, 20, 15, 20};
        _mineral_ = new[] {4, 3, 3, 3, 3, 3, 0, 3, 3, 5, 5};
        _cristal_ = new[] {5, 5, 4, 3, 2, 0, 3, 1, 5, 1, 5, 3, 5};
        for (int k=0; k<4;k++)
        {
            less[k].interactable = false;
            more[k].interactable = true;
        }
        if (swordman_ == 1) {Buildings[0].gameObject.SetActive(true); Build_[0].interactable = false;}
        if (archer_ == 1) {Buildings[1].gameObject.SetActive(true); Build_[1].interactable = false;}
        if (cavarly_ == 1) {Buildings[2].gameObject.SetActive(true); Build_[2].interactable = false;}
        if (magic_ == 1) {Buildings[3].gameObject.SetActive(true); Build_[3].interactable = false;}
        if (market_ == 1) {Buildings[4].gameObject.SetActive(true); Build_[4].interactable = false;}
        if (gold_m == 1) {Buildings[5].gameObject.SetActive(true); Build_[5].interactable = false; add5 = true;}
        if (board_m == 1) {Buildings[6].gameObject.SetActive(true); Build_[6].interactable = false;add6= true;}
        if (stone_m== 1) {Buildings[7].gameObject.SetActive(true); Build_[7].interactable = false; add7 = true;}
        if (mineral_m == 1) {Buildings[8].gameObject.SetActive(true); Build_[8].interactable = false; add8 = true;}
        if (cristal_m == 1) {Buildings[9].gameObject.SetActive(true); Build_[9].interactable = false; add9 = true;}
    }
    private void Update()
    {
        TimerGoldCastle();
        Timer();
        TimerArm();
        tasks();
        if (Int32.Parse(levelPlayer.text) >= 3) map_Battle.interactable = true;
    }
    private void HardLevel()
    {
        if (MainMenu.level == 1)
        {
            _goldNum = 50000;
            _stoneNum = 50;
            _boardNum = 50;
            _crystalNum = 25;
            _mineralNum = 25;
        }
        else if (MainMenu.level == 2)
        {
            _goldNum = 30000;
            _stoneNum = 30;
            _boardNum = 30;
            _crystalNum = 20;
            _mineralNum = 20;
        }
        else if (MainMenu.level ==3)
        {
            _goldNum = 20000;
            _stoneNum = 20;
            _boardNum = 20;
            _crystalNum = 15;
            _mineralNum = 15;
        }
        _level = 1;
        _power= 1000;
    }
    public void SaveInFile()
     {
         StreamWriter sw = new StreamWriter(path + "/" + nameFile);
         sw.WriteLine(levelPlayer.text); 
         sw.WriteLine(powerPlayer.text);
         sw.WriteLine(_spriteNum);
         sw.WriteLine(Swordman_num); 
         sw.WriteLine(Archer_num);
         sw.WriteLine(Cavarly_num);
         sw.WriteLine(Magic_num);
         sw.WriteLine(gold.text);
         sw.WriteLine(board.text);
         sw.WriteLine(stone.text); 
         sw.WriteLine(mineral.text);
         sw.WriteLine(crystal.text);
         sw.WriteLine(swordman_);
         sw.WriteLine(archer_);
         sw.WriteLine(cavarly_);
         sw.WriteLine(magic_);
         sw.WriteLine(market_);
         sw.WriteLine(gold_m);
         sw.WriteLine(board_m);
         sw.WriteLine(stone_m);
         sw.WriteLine(mineral_m);
         sw.WriteLine(cristal_m);
         sw.WriteLine(castlelevel.text); 
         sw.WriteLine(swordsmanlevel.text);
         sw.WriteLine(archerlevel.text);
         sw.WriteLine(cavalrylevel.text); 
         sw.WriteLine(magicallevel.text);
         sw.WriteLine(marketlevel.text);
         sw.WriteLine(goldlevel.text); 
         sw.WriteLine(boardlevel.text); 
         sw.WriteLine(stonelevel.text);
         sw.WriteLine(minerallevel.text);
         sw.WriteLine(crystallevel.text);
         sw.WriteLine(num);
         sw.WriteLine(System.Convert.ToInt32(task1));
         sw.WriteLine(System.Convert.ToInt32(task2));
         sw.WriteLine(System.Convert.ToInt32(task2));
         sw.Close();
     }
    public void ReadFromFile()
    {
        StreamReader sr = new StreamReader(path + "/" + nameFile);
        _level= Int32.Parse(sr.ReadLine());
        _power = Int32.Parse(sr.ReadLine());
        _spriteNum = Int32.Parse(sr.ReadLine());
        Swordman_num = Int32.Parse(sr.ReadLine());
        Archer_num = Int32.Parse(sr.ReadLine());
        Cavarly_num = Int32.Parse(sr.ReadLine());
        Magic_num = Int32.Parse(sr.ReadLine());
        _goldNum = Int32.Parse(sr.ReadLine());
        _boardNum = Int32.Parse(sr.ReadLine());
        _stoneNum = Int32.Parse(sr.ReadLine());;
        _mineralNum = Int32.Parse(sr.ReadLine());
        _crystalNum = Int32.Parse(sr.ReadLine());
        swordman_ = Int32.Parse(sr.ReadLine());
        archer_ = Int32.Parse(sr.ReadLine());
        cavarly_ = Int32.Parse(sr.ReadLine());
        magic_ = Int32.Parse(sr.ReadLine());
        market_ = Int32.Parse(sr.ReadLine());
        gold_m = Int32.Parse(sr.ReadLine());
        board_m = Int32.Parse(sr.ReadLine());
        stone_m = Int32.Parse(sr.ReadLine());
        mineral_m = Int32.Parse(sr.ReadLine());
        cristal_m = Int32.Parse(sr.ReadLine());
        castlelevel.text = sr.ReadLine();
        swordsmanlevel.text = sr.ReadLine();
        archerlevel.text = sr.ReadLine();
        cavalrylevel.text = sr.ReadLine();
        magicallevel.text = sr.ReadLine();
        marketlevel.text = sr.ReadLine();
        goldlevel.text = sr.ReadLine();
        boardlevel.text = sr.ReadLine();
        stonelevel.text = sr.ReadLine();
        minerallevel.text = sr.ReadLine();
        crystallevel.text = sr.ReadLine();
        num = Int32.Parse(sr.ReadLine());
        task1_ = Int32.Parse(sr.ReadLine());
        task2_ = Int32.Parse(sr.ReadLine());
        task3_ = Int32.Parse(sr.ReadLine());
        sr.Close();
        if (task1_ == 0) task1 = false;else task1 = true;
        if (task2_ == 0) task1 = false;else task2 = true;
        if (task3_ == 0) task1 = false;else task3 = true;
        StartGame();
        readfile = false;
    }
    public void SaveData2()
    {
        PlayerPrefs.SetInt("saved", 1);
        curSwordman = Swordman_num; curArcher = Archer_num; curCavarly = Cavarly_num; curMagic = Magic_num;
        PlayerPrefs.SetInt("PlayerLevel", Int32.Parse(levelPlayer.text));
        PlayerPrefs.SetInt("PowerPlayer", Int32.Parse(powerPlayer.text));
        PlayerPrefs.SetInt("NumImage", _spriteNum);
        PlayerPrefs.SetInt("MainGold", Int32.Parse(gold.text));
        PlayerPrefs.SetInt("MainBoard", Int32.Parse(board.text));
        PlayerPrefs.SetInt("MainStone", Int32.Parse(stone.text));
        PlayerPrefs.SetInt("MainMineral", Int32.Parse(mineral.text));
        PlayerPrefs.SetInt("MainCristal", Int32.Parse(crystal.text));
        PlayerPrefs.SetInt("SwordManBuild", swordman_);
        PlayerPrefs.SetInt("ArcherBuild", archer_);
        PlayerPrefs.SetInt("CavarlyBuild", cavarly_);
        PlayerPrefs.SetInt("MagicBuild", magic_);
        PlayerPrefs.SetInt("MarketBuild", market_);
        PlayerPrefs.SetInt("GoldBuild", gold_m);
        PlayerPrefs.SetInt("BoardBuild", board_m);
        PlayerPrefs.SetInt("StoneBuild", stone_m);
        PlayerPrefs.SetInt("MineralBuild", mineral_m);
        PlayerPrefs.SetInt("CristalBuild", cristal_m);
        PlayerPrefs.SetInt("CastleLevel", Int32.Parse(castlelevel.text));
        PlayerPrefs.SetInt("SwordManLevel", Int32.Parse(swordsmanlevel.text));
        PlayerPrefs.SetInt("ArcherLevel", Int32.Parse(archerlevel.text));
        PlayerPrefs.SetInt("CavarlyLevel", Int32.Parse(cavalrylevel.text));
        PlayerPrefs.SetInt("MagicLevel", Int32.Parse(magicallevel.text));
        PlayerPrefs.SetInt("MarketLevel", Int32.Parse(marketlevel.text));
        PlayerPrefs.SetInt("GoldLevel", Int32.Parse(goldlevel.text));
        PlayerPrefs.SetInt("BoardLevel", Int32.Parse(boardlevel.text));
        PlayerPrefs.SetInt("StoneLevel", Int32.Parse(stonelevel.text));
        PlayerPrefs.SetInt("MineralLevel", Int32.Parse(minerallevel.text));
        PlayerPrefs.SetInt("CristalLevel", Int32.Parse(crystallevel.text));
        PlayerPrefs.SetInt("Buy_Swordman", System.Convert.ToInt32(Build_[0].interactable));
        PlayerPrefs.SetInt("Num_task", num);
        PlayerPrefs.SetInt("task1", System.Convert.ToInt32(task1));
        PlayerPrefs.SetInt("task2", System.Convert.ToInt32(task2));
        PlayerPrefs.SetInt("task3", System.Convert.ToInt32(task3));
    }
    void LoadData2()
    {
        if (PlayerPrefs.HasKey("PlayerLevel")) _level = PlayerPrefs.GetInt("PlayerLevel");
        if (PlayerPrefs.HasKey("PowerPlayer")) _power = PlayerPrefs.GetInt("PowerPlayer") - map.minusPower_;
        if (PlayerPrefs.HasKey("NumImage")) _spriteNum = PlayerPrefs.GetInt("NumImage");
        if (PlayerPrefs.HasKey("MainGold")) _goldNum = PlayerPrefs.GetInt("MainGold") + map.gold;
        if (PlayerPrefs.HasKey("MainBoard")) _boardNum = PlayerPrefs.GetInt("MainBoard") + map.board;
        if (PlayerPrefs.HasKey("MainStone")) _stoneNum = PlayerPrefs.GetInt("MainStone") + map.stone;
        if (PlayerPrefs.HasKey("MainMineral")) _mineralNum = PlayerPrefs.GetInt("MainMineral") + map.mineral;
        if (PlayerPrefs.HasKey("MainCristal")) _crystalNum = PlayerPrefs.GetInt("MainCristal") + map.cristal;
        if (PlayerPrefs.HasKey("SwordBuild")) swordman_ = PlayerPrefs.GetInt("SwordManBuild");
        if (PlayerPrefs.HasKey("ArcherBuild")) archer_ = PlayerPrefs.GetInt("ArcherBuild");
        if (PlayerPrefs.HasKey("CavarlyBuild")) cavarly_ = PlayerPrefs.GetInt("CavarlyBuild");
        if (PlayerPrefs.HasKey("MagicBuild")) magic_ = PlayerPrefs.GetInt("MagicBuild");
        if (PlayerPrefs.HasKey("MarketBuild")) market_ = PlayerPrefs.GetInt("MarketBuild");
        if (PlayerPrefs.HasKey("GoldBuild")) gold_m = PlayerPrefs.GetInt("GoldBuild");
        if (PlayerPrefs.HasKey("BoardBuild")) board_m = PlayerPrefs.GetInt("BoardBuild");
        if (PlayerPrefs.HasKey("StoneBuild")) stone_m = PlayerPrefs.GetInt("StoneBuild");
        if (PlayerPrefs.HasKey("MineralBuild")) mineral_m = PlayerPrefs.GetInt("MineralBuild");
        if (PlayerPrefs.HasKey("CristalBuild")) cristal_m = PlayerPrefs.GetInt("CristalBuild"); ;
        if (PlayerPrefs.HasKey("CastleLevel")) castlelevel.text = System.Convert.ToString(PlayerPrefs.GetInt("CastleLevel"));
        if (PlayerPrefs.HasKey("SwordManLevel")) swordsmanlevel.text = System.Convert.ToString(PlayerPrefs.GetInt("SwordManLevel"));
        if (PlayerPrefs.HasKey("ArcherLevel")) archerlevel.text = System.Convert.ToString(PlayerPrefs.GetInt("ArcherLevel"));
        if (PlayerPrefs.HasKey("CavarlyLevel")) cavalrylevel.text = System.Convert.ToString(PlayerPrefs.GetInt("CavarlyLevel"));
        if (PlayerPrefs.HasKey("MagicLevel")) magicallevel.text = System.Convert.ToString(PlayerPrefs.GetInt("MagicLevel"));
        if (PlayerPrefs.HasKey("MarketLevel")) marketlevel.text = System.Convert.ToString(PlayerPrefs.GetInt("MarketLevel"));
        if (PlayerPrefs.HasKey("GoldLevel")) goldlevel.text = System.Convert.ToString(PlayerPrefs.GetInt("GoldLevel"));
        if (PlayerPrefs.HasKey("BoardLevel")) boardlevel.text = System.Convert.ToString(PlayerPrefs.GetInt("BoardLevel"));
        if (PlayerPrefs.HasKey("StoneLevel")) stonelevel.text = System.Convert.ToString(PlayerPrefs.GetInt("StoneLevel"));
        if (PlayerPrefs.HasKey("MineralLevel")) minerallevel.text = System.Convert.ToString(PlayerPrefs.GetInt("MineralLevel"));
        if (PlayerPrefs.HasKey("CristalLevel")) crystallevel.text = System.Convert.ToString(PlayerPrefs.GetInt("CristalLevel")); ;
        if (PlayerPrefs.HasKey("Num_task")) num = PlayerPrefs.GetInt("Num_task");
        if (PlayerPrefs.HasKey("task1")) task1 = System.Convert.ToBoolean(PlayerPrefs.GetInt("task1"));
        if (PlayerPrefs.HasKey("task2")) task2 = System.Convert.ToBoolean(PlayerPrefs.GetInt("task2"));
        if (PlayerPrefs.HasKey("task3")) task3 = System.Convert.ToBoolean(PlayerPrefs.GetInt("task3"));
        map.gold = map.board = map.stone = map.mineral = map.cristal = 0;
        Swordman_num = curSwordman;
        Archer_num = curArcher;
        Magic_num = curMagic;
        Cavarly_num = curCavarly;
        Debug.Log(curCavarly);
        Debug.Log(Cavarly_num);
    }
    private void tasks()
    {
        if (task1 == true && task2 == true && task3 == true)
        {
            Get_Gift.interactable = true;
            if (click == true)
            {
                task1 = task2 = task3 = click = false; num++;
                gold.text = (Int32.Parse(gold.text) + Int32.Parse(gift_res[0].text)).ToString();
                board.text = (Int32.Parse(stone.text) + Int32.Parse(gift_res[1].text)).ToString();
                stone.text = (Int32.Parse(board.text) + Int32.Parse(gift_res[2].text)).ToString(); 
                mineral.text = (Int32.Parse(mineral.text) + Int32.Parse(gift_res[3].text)).ToString(); 
                crystal.text = (Int32.Parse(crystal.text) + Int32.Parse(gift_res[4].text)).ToString();
            }
            
        }
        else if (task1 != true || task2 == true || task3 ==true) Get_Gift.interactable = false;
        task_set(num);
        if (task1 == true) task_cheak[0].sprite = tasks_[1];
        else task_cheak[0].sprite = tasks_[0];
        if (task2 == true) task_cheak[1].sprite = tasks_[1];
        else task_cheak[1].sprite = tasks_[0];
        if (task3 == true) task_cheak[2].sprite = tasks_[1];
        else task_cheak[2].sprite = tasks_[0];
    }
    public void GetGift() { click = true;}
    private void task_set(int task_set_num)
    {
        switch (task_set_num)
        {
            case 1:
            {
                if (Int32.Parse(levelPlayer.text) >= 3) {task1 = true; }
                if (Int32.Parse(powerPlayer.text) >= 30000){task2 = true; }
                if (Build_[6].interactable == false) {task3 = true;}
                break;
            }
            case 2:
            {
                task_text[0].text = "Улучшить замок до 8 уровня";
                task_text[1].text = "Обучить 30 рыцарей";
                task_text[2].text = "Построить рудниковую шахту";
                gift_res[0].text = "2500";
                gift_res[1].text = gift_res[2].text = "15";
                gift_res[3].text = "2";
                if (castlelevel.text == "8") {task1 = true; }
                if (Swordman_num >= 30){task2 = true;}
                if (Build_[8].interactable == false) {task3 = true;}
                break;
            }
            case 3:
            {
                task_text[0].text = "Улучшить рынок до 5 уровня";
                task_text[1].text = "Обучить 30 магов";
                task_text[2].text = "Построить кристальную шахту";
                gift_res[0].text = "3000";
                gift_res[1].text = "10";
                gift_res[2].text = "15";
                gift_res[3].text = "3";
                gift_res[4].text = "2";
                if (Int32.Parse(marketlevel.text) >= 5) {task1 = true;}
                if (Magic_num >= 30){task2 = true; }
                if (Build_[9].interactable == false) {task3 = true;}
                break;
            }
        }
    }
    void TimerGoldCastle()
    {
        if (add == true) if (TimergoldCastle <= gold_) TimergoldCastle += 15*Time.deltaTime;
        if (Math.Round(TimergoldCastle) == gold_)
        {
            gold.text = (Int32.Parse(gold.text) + Math.Round(TimergoldCastle)).ToString();
            TimergoldCastle = 0;
            add = true;
            TimerGoldCastle();
        }
    }
    void Timer()
    {
        if (add5 == true) { if (timergold <= gold_2) timergold += (float)6.5*Time.deltaTime; }
        if (add6 == true) { if (timerBoard <= board_) timerBoard += (float)0.2*Time.deltaTime; }
        if (add7 == true) { if (timerstone<= stone_) timerstone += (float)0.2*Time.deltaTime; }
        if (add8 == true) { if (timermineral<= mineral_) timermineral += (float)0.08*Time.deltaTime; }
        if (add9 == true) { if (timercrystal<= cristal) timercrystal += (float)0.08*Time.deltaTime; }
        if (Math.Round(timergold) == gold_2) {
            gold.text = (Int32.Parse(gold.text) + Math.Round(timergold)).ToString();
            timergold = 0;
            add5 = true;
            Timer(); }
        if (Math.Round(timerBoard) == board_) {
            board.text = (Int32.Parse(board.text) + Math.Round(timerBoard)).ToString();
            timerBoard = 0;
            add6 = true;
            Timer(); }
        if (Math.Round(timerstone) == stone_)
        {
            stone.text = (Int32.Parse(stone.text) + Math.Round(timerstone)).ToString();
            timerstone = 0;
            add7 = true;
            Timer();
        }
        if (Math.Round(timermineral) == mineral_) {
            mineral.text = (Int32.Parse(mineral.text) + Math.Round(timermineral)).ToString();
            timermineral = 0;
            add8 = true;
            Timer(); }
        if (Math.Round(timercrystal) == cristal)
        {
            crystal.text = (Int32.Parse(crystal.text) + Math.Round(timercrystal)).ToString();
            timercrystal = 0;
            add9 = true;
            Timer();
        }
    }
    void TimerArm()
    {
        if (add1 == true) { 
            if (timerSwordman < swordsman) {timerSwordman += (float)0.5*Time.deltaTime;}
            else if (Math.Round(timerSwordman) == swordsman)
            {
                Swordman_num += (int)Math.Round(timerSwordman);
                NumSwordman.text = (Swordman_num).ToString() + " чел.";
                powerPlayer.text = (Int32.Parse(powerPlayer.text) + 20 * swordsman).ToString();
                timerSwordman = 0;
                add1 = false; 
            }
        }
        if (add2 == true) { 
            if (timerArcher < archer) {timerArcher += (float)0.5*Time.deltaTime;}
            else if (Math.Round(timerArcher) == archer)
            {
                Archer_num += (int)Math.Round(timerArcher);
                NumArcher.text = (Archer_num).ToString() + " чел.";
                powerPlayer.text  = (Int32.Parse(powerPlayer.text ) + 25 * (int) Math.Round(timerArcher)).ToString();
                timerArcher = 0;
                add2 = false; 
            }
        }
        if (add3 == true) { 
            if (timerCavalry < cavalry) {timerCavalry += (float)0.3*Time.deltaTime;}
            else if (Math.Round(timerCavalry) == cavalry)
            {
                Cavarly_num += (int)Math.Round(timerCavalry);
                NumCavalry.text = Cavarly_num.ToString() + " чел.";
                powerPlayer.text  = (Int32.Parse(powerPlayer.text ) + 30 * (int) Math.Round(timerCavalry)).ToString();
                timerCavalry = 0;
                add3 = false; 
            }
        }
        if (add4 == true) { 
            if (timerMagic < magician) {timerMagic += (float)0.15*Time.deltaTime; Debug.Log(timerMagic);}
            else if (Math.Round(timerMagic) == magician)
            {
                Magic_num+= (int)Math.Round(timerMagic);
                NumMagical.text = (Magic_num).ToString() + " чел.";
                powerPlayer.text = (Int32.Parse(powerPlayer.text ) + 35 * (int) Math.Round(timerMagic)).ToString();
                timerMagic = 0;
                add4 = false; 
            }
        }
    }
    public void Army()
    {
        if (idn == 2) add1 = true;
        if (idn == 3) add2 = true; 
        if (idn == 4) add3 = true;
        if (idn == 5) add4 = true; 
    }
    private void PlayerLevel(int levelnext)
    {
        if (Int16.Parse(levelPlayer.text) != maxLevelPlayer)
        { 
            levelPlayer.text = (Int16.Parse(levelPlayer.text) + levelnext).ToString();
            Player_.text = "уровень: " + levelPlayer.text;
        }
    }
    private void PlayerPower(int pw)
    {
        _power += pw;
        if (_power<1000000) { powerPlayer.text = (_power).ToString(); }
        else
        {
            powerPlayer.text = ((int)_power/1000).ToString() + "K"; 
        }
        Power_.text = "мощь: " + (_power).ToString();
    }
    private void HP(int idn)
    {
        if (idn == 1)
        {
            hp += 150;
        }
        else if (idn >1 && idn < 6)
        {
            hp += 100;
        }
        else if (idn > 6)
        {
            hp += 50;
        }
        if (hp >= maxhp)
        {
            temp = (int) (hp / maxhp);
            hp = hp - maxhp*temp;
            PlayerLevel(temp);
            maxhp *= 2;
        }
    }
    public void Info(int id)
    {
        if (id == 1) res = 0;
        else if (id >= 2 && id <= 5) res = 1;
        else if (id == 6) res = 2;
        else if (id >= 7 && id <= 11) res = 3;
        name_of_bilding[res].text = nameBuild[id-1];
        if (id >= 1 && id<6) info[res].text = descBuild[id-1] + res_[id-1];
        else if (id == 6) info[res].text = descBuild[id-1];
        else if (id >= 7 && id <= 11) info[res].text = descBuild[id-1] + res_[id-2];
        level_of_building[res].text = level_Build[id-1].text;
        if (Int32.Parse(level_Build[id-1].text) != maxlevelCastle) nextLevel[res].text = (Int32.Parse(level_Build[res].text) + 1).ToString();
        else {
            nextLevel[res].text = "max";
            maxlevelbut[res].interactable = false;
        }
        idn = id;
        newImage(id);
        quantity_of_gold[res].text = _gold_[id-1].ToString();
        quantity_of_stone[res].text = _stone_[id-1].ToString();
        quantity_of_tree[res].text = _board_[id-1].ToString();
        quantity_of_crist[res].text = _mineral_[id-1].ToString();
        quantity_of_mineral[res].text = _cristal_[id-1].ToString();
        cheak(res);
    }
    private void cheak(int idres)
    {
        if (Int32.Parse(gold.text) < Int32.Parse(quantity_of_gold[idres].text))
        {
            quantity_of_gold[idres].color = Color.red;
            i++;
        }
        if (Int32.Parse(stone.text) < Int32.Parse(quantity_of_stone[idres].text))
        {
            quantity_of_stone[idres].color = Color.red;
            i++;
        }
        if (Int32.Parse(board.text) < Int32.Parse(quantity_of_tree[idres].text))
        {
            quantity_of_tree[idres].color = Color.red;
            i++;
        }
        if (Int32.Parse(mineral.text) < Int32.Parse(quantity_of_mineral[idres].text))
        {
            quantity_of_mineral[idres].color = Color.red;
            i++;
        }
        if (Int32.Parse(crystal.text) < Int32.Parse(quantity_of_crist[idres].text))
        {
            quantity_of_crist[idres].color = Color.red;
            i++;
        }
        if (i > 0 && idres >=0 && idres <4)
        {
            next[idres].interactable = false;
        }
        else if ((i > 0 && idres == 4))
        {
            buyButton.interactable = false;
        }
        i = 0;
    }
    public void BuyNewBuild(int idk)
    {
        nameBuilding.text = nameBuild[idk+1];
        if (idk >= 0 && idk<4) inf.text = descBuild[idk+1] + res_[idk+1];
        else if (idk ==4) inf.text = descBuild[idk+1];
        else if (idk >= 5 && idk <= 9) inf.text = descBuild[idk+1] + res_[idk];
        idBuild = idk +1;
        old2.sprite = image_of_building[idBuild];
        quantity_of_gold[4].text = _gold_[idk].ToString();
        quantity_of_stone[4].text = _stone_[idk].ToString();
        quantity_of_tree[4].text = _board_[idk].ToString();
        quantity_of_crist[4].text = _mineral_[idk].ToString();
        quantity_of_mineral[4].text = _cristal_[idk].ToString();
        cheak(4);
    }
    void DescriptionA()
    {
        description[0].text = "Рыцари\n +" + swordsman.ToString() + " шт/3 часа";
        description[1].text = "Лучники\n +" + archer.ToString() + " шт/3 часа";
        description[2].text = "Конница\n +" + cavalry.ToString() + " шт/3 часа";
        description[3].text = "Маги\n +" + magician.ToString() + " шт/3 часа";
        description[5].text = "Золотая шахта\n +" + gold_2.ToString() + " шт/2 часа";
        description[6].text = "Лесопилка\n +" + board_.ToString() + " шт/2 часа";
        description[7].text = "Каменоломня\n +" + stone_.ToString() + " шт/2 часа";
        description[8].text = "Рудниковая шахта\n +" + mineral_.ToString() + " шт/2 часа";
        description[9].text = "Кристальная шахта\n +" + cristal.ToString() + " шт/2 часа";
        if (Int32.Parse(levelPlayer.text) < 4) map_Battle.interactable = false;
        else map_Battle.interactable = true;
    }
    public void Buy()
    {
        BuyBuilding(2);
        if (i == 0)
        {
            Buildings[idBuild-1].gameObject.SetActive(true);
            if (idBuild - 1 == 0) swordman_ = 1;
            if (idBuild - 1 == 1) archer_ = 1;
            if (idBuild - 1 == 2) cavarly_ = 1;
            if (idBuild - 1 == 3) magic_ = 1;
            if (idBuild - 1 == 4) market_ = 1;
            if (idBuild - 1 == 5) {add5 = true; gold_m = 1;}
            if (idBuild - 1 == 6) {add6 = true; board_m = 1;}
            if (idBuild - 1 == 7) {add7 = true; stone_m = 1;} 
            if (idBuild - 1 == 8) {add8 = true; mineral_m = 1;} 
            if (idBuild - 1 == 9) {add9 = true; cristal_m = 1;}
            Timer(); 
            Build_[idBuild - 1].interactable = false; 
        }
        PlayerPower(powerArmiBuild);
    }
    private void newImage(int idn)
    {
        if (idn == 1){ 
            if (Int16.Parse(castlelevel.text) >= 9)
        {
            oldImg[idn - 1].sprite = newSprite[idn - 1];
            old[0].sprite = newSprite[idn - 1];
        }
        else
        {
            oldImg[idn - 1].sprite = image_of_building[idn - 1];
            old[0].sprite = image_of_building[idn - 1];
        } 
        }
        if (idn == 2)
        {
            if (Int16.Parse(swordsmanlevel.text) >= 4)
        {
            oldImg[idn-1].sprite = newSprite[idn -1];
            old[1].sprite = newSprite[idn - 1];
        }
        else
        {
            oldImg[idn - 1].sprite = image_of_building[idn - 1];
            old[1].sprite = image_of_building[idn - 1];
        }
        }
        if (idn == 3) {
            if (Int16.Parse(archerlevel.text) >= 4)
        {
            oldImg[idn-1].sprite = newSprite[idn -1];
            old[1].sprite = newSprite[idn - 1];
        }
        else
        {
            oldImg[idn - 1].sprite = image_of_building[idn - 1];
            old[1].sprite = image_of_building[idn - 1];
        }}
        if (idn == 4) {
            if ( Int16.Parse(cavalrylevel.text) >= 4)        {
            oldImg[idn-1].sprite = newSprite[idn -1];
            old[1].sprite = newSprite[idn - 1];
        }
        else
        {
            oldImg[idn - 1].sprite = image_of_building[idn - 1];
            old[1].sprite = image_of_building[idn - 1];
        }}
        if (idn == 5) {
            if ( Int16.Parse(archerlevel.text) >= 4)        {
            oldImg[idn-1].sprite = newSprite[idn -1];
            old[1].sprite = newSprite[idn - 1];
        }
        else
        {
            oldImg[idn - 1].sprite = image_of_building[idn - 1];
            old[1].sprite = image_of_building[idn - 1];
        }}
        if (idn == 6) {
            if(Int16.Parse(cavalrylevel.text) >= 4)        {
            oldImg[idn-1].sprite = newSprite[idn -1];
            old[2].sprite = newSprite[idn - 1];
        }
        else
        {
            oldImg[idn - 1].sprite = image_of_building[idn - 1];
            old[2].sprite = image_of_building[idn - 1];
        }}
        if (idn == 7) {
            if ( Int16.Parse(goldlevel.text) >= 4)        {
            oldImg[idn-1].sprite = newSprite[idn -1];
            old[3].sprite = newSprite[idn - 1];
        }
        else
        {
            oldImg[idn - 1].sprite = image_of_building[idn - 1];
            old[3].sprite = image_of_building[idn - 1];
        }}
        if (idn == 8){
            if( Int16.Parse(boardlevel.text) >= 4)        {
            oldImg[idn-1].sprite = newSprite[idn -1];
            old[3].sprite = newSprite[idn - 1];
        }
        else
        {
            oldImg[idn - 1].sprite = image_of_building[idn - 1];
            old[3].sprite = image_of_building[idn - 1];
        }}
        if (idn == 9) {
            if( Int16.Parse(stonelevel.text) >= 4)        {
            oldImg[idn-1].sprite = newSprite[idn -1];
            old[3].sprite = newSprite[idn - 1];
        }
        else
        {
            oldImg[idn - 1].sprite = image_of_building[idn - 1];
            old[3].sprite = image_of_building[idn - 1];
        }}
        if (idn == 10) {
            if (Int16.Parse(minerallevel.text) >= 4)        {
            oldImg[idn-1].sprite = newSprite[idn -1];
            old[3].sprite = newSprite[idn - 1];
        }
        else
        {
            oldImg[idn - 1].sprite = image_of_building[idn - 1];
            old[3].sprite = image_of_building[idn - 1];
        }}
        if (idn == 11) {
            if (Int16.Parse(crystallevel.text) >= 4)        {
            oldImg[idn-1].sprite = newSprite[idn -1];
            old[3].sprite = newSprite[idn - 1];
        }
        else
        {
            oldImg[idn - 1].sprite = image_of_building[idn - 1];
            old[3].sprite = image_of_building[idn - 1];
        }}
    }
    private void Level()
    {
        if (idn == 1 && Int16.Parse(castlelevel.text) != maxlevelCastle)
        {
            castlelevel.text = (Int16.Parse(castlelevel.text) + 1).ToString();
            PlayerPower(powerCastl);
            powerCastl += 4000;
            res_[idn-1] = gold_ += 500; 
        }
        if (idn == 2 && Int16.Parse(swordsmanlevel.text) != maxlevelBuild)
        {
            swordsmanlevel.text = (Int16.Parse(swordsmanlevel.text) + 1).ToString();            
            PlayerPower(powerArmiBuild);
            powerArmiBuild +=2000;
            res_[idn-1] = swordsman+= 5;
        }
        if (idn == 3 && Int16.Parse(archerlevel.text) != maxlevelBuild)
        {
            archerlevel.text = (Int16.Parse(archerlevel.text) + 1).ToString();
            PlayerPower(powerArmiBuild2);
            powerArmiBuild2 +=2000;
            res_[idn-1] = archer += 5;
        }
        if (idn == 4 && Int16.Parse(cavalrylevel.text) != maxlevelBuild)
        {
            cavalrylevel.text = (Int16.Parse(cavalrylevel.text) + 1).ToString();
            PlayerPower(powerArmiBuild3);
            powerArmiBuild3 +=2000;
            res_[idn-1] = cavalry += 3;
        }
        if (idn == 5 && Int16.Parse(magicallevel.text) != maxlevelBuild)
        {
            magicallevel.text = (Int16.Parse(magicallevel.text) + 1).ToString();
            PlayerPower(powerArmiBuild4);
            powerArmiBuild4 +=2000;
            res_[idn-1] = magician += 2;
        }
        if (idn == 6 && Int16.Parse(marketlevel.text) != maxlevelBuild)
        {
            marketlevel.text = (Int16.Parse(marketlevel.text) + 1).ToString();
            PlayerPower(powerRes); 
            powerRes +=2000;
        }
        if (idn == 7 && Int16.Parse(goldlevel.text) != maxlevelBuild)
        {
            goldlevel.text = (Int16.Parse(goldlevel.text) + 1).ToString();
            PlayerPower(powerRes2);
            powerRes2 +=1000;
            res_[idn-2] = gold_2 += 100;
        }
        if (idn == 8 && Int16.Parse(boardlevel.text) != maxlevelBuild)
        {
            boardlevel.text = (Int16.Parse(boardlevel.text) + 1).ToString();
            PlayerPower(powerRes3);
            powerRes3 +=1000;
            res_[idn-2] = board_ += 3;
        }
        if (idn == 9 && Int16.Parse(stonelevel.text) != maxlevelBuild)
        {
            stonelevel.text = (Int16.Parse(stonelevel.text) + 1).ToString();
            PlayerPower(powerRes4);
            powerRes4 +=1000;
            res_[idn-2] = stone_ += 3;
        }
        if (idn == 10 && Int16.Parse(minerallevel.text) != maxlevelBuild)
        {
            minerallevel.text = (Int16.Parse(minerallevel.text) + 1).ToString();
            PlayerPower(powerRes5);
            powerRes5 +=1000;
            res_[idn-2] = mineral_ += 1;
        }
        if (idn == 11 && Int16.Parse(crystallevel.text) != maxlevelBuild)
        {
            crystallevel.text = (Int16.Parse(crystallevel.text) + 1).ToString();
            PlayerPower(powerRes6);
            powerRes6 +=1000;
            res_[idn-2] = cristal += 1;
        }
    }
    public void BuyBuilding(int idButton)
    {
        if (idButton == 1){ 
            gold.text = (Int32.Parse(gold.text) - Int32.Parse(quantity_of_gold[res].text)).ToString();
            stone.text = (Int32.Parse(stone.text) - Int32.Parse(quantity_of_stone[res].text)).ToString(); 
            board.text = (Int32.Parse(board.text) - Int32.Parse(quantity_of_tree[res].text)).ToString(); 
            mineral.text = (Int32.Parse(mineral.text) - Int32.Parse(quantity_of_mineral[res].text)).ToString(); 
            crystal.text = (Int32.Parse(crystal.text) - Int32.Parse(quantity_of_crist[res].text)).ToString(); 
            newImage(idn); 
            Level();
            Debug.Log("hi");
        }
        else if (idButton == 2)
        {
            gold.text = (Int32.Parse(gold.text) - Int32.Parse(quantity_of_gold[4].text)).ToString();
            stone.text = (Int32.Parse(stone.text) - Int32.Parse(quantity_of_stone[4].text)).ToString();
            board.text = (Int32.Parse(board.text) - Int32.Parse(quantity_of_tree[4].text)).ToString(); 
            mineral.text = (Int32.Parse(mineral.text) - Int32.Parse(quantity_of_mineral[4].text)).ToString(); 
            crystal.text = (Int32.Parse(crystal.text) - Int32.Parse(quantity_of_crist[4].text)).ToString();
        }        
        HP(idn);
    }
    public void Market(int butt)
    {
        if (butt == 9 || butt == 11 || butt ==  13 || butt == 15)
        {
            if (butt == 9) butt = 0;
            else if (butt == 11) butt = 1;
            else if (butt == 13) butt = 2;
            else if (butt == 15) butt = 3;
            CheakButton(butt);
            if (less[butt].interactable==true)
            {
                if (butt == 0) { Board_num -= 1;Gold_num -= 250; MarketRes[butt].text = Board_num.ToString();}
                if (butt == 1) { Stone_num -= 1; Gold_num -= 250; MarketRes[butt].text = Stone_num.ToString();}
                if (butt == 2) { Mineral_num -= 1; Gold_num -= 800; MarketRes[butt].text = Mineral_num.ToString();}
                if (butt == 3) { Cristal_num -= 1; Gold_num -= 800; MarketRes[butt].text = Cristal_num.ToString();}
            }
            Gold_to_market.text = Gold_num.ToString();
           CheakButton(butt);
        }
        if (butt == 10 || butt == 12 || butt == 14 || butt == 16)
        {
            if (butt == 10) butt = 0;
            else if (butt==12) butt = 1;
            else if (butt == 14) butt = 2;
            else if (butt == 16) butt = 3;
            CheakButton(butt);
            if (more[butt].interactable==true){
                if (butt == 0) { Board_num += 1;Gold_num += 250; MarketRes[butt].text = Board_num.ToString();}
                if (butt == 1) { Stone_num += 1; Gold_num += 250; MarketRes[butt].text = Stone_num.ToString();}
                if (butt == 2) { Mineral_num += 1; Gold_num += 800; MarketRes[butt].text = Mineral_num.ToString();}
                if (butt == 3) { Cristal_num += 1; Gold_num += 800; MarketRes[butt].text = Cristal_num.ToString();}
            }
            Gold_to_market.text = Gold_num.ToString();
            CheakButton(butt);
        }
        if (butt == 8)
        {
            for (int y=0; y < 4; y++){less[y].interactable = false;}
            gold.text = (Int32.Parse(gold.text) - Gold_num).ToString();
            board.text = (Int32.Parse(board.text) + Board_num).ToString();
            stone.text = (Int32.Parse(stone.text) + Stone_num).ToString(); 
            mineral.text = (Int32.Parse(mineral.text) + Mineral_num).ToString(); 
            crystal.text = (Int32.Parse(crystal.text) + Cristal_num).ToString();
            Gold_num = Stone_num = Board_num = Mineral_num = Cristal_num = 0;
            Gold_to_market.text = MarketRes[0].text = MarketRes[1].text = MarketRes[2].text = MarketRes[3].text = 0.ToString();
        }
    }
    private void CheakButton(int k_)
    {
         if (Int32.Parse(MarketRes[k_].text) <=0) less[k_].interactable = false;
         else less[k_].interactable = true;
         if (Gold_num > Int32.Parse(gold.text))
         {
             for (int k__=0; k__<4;k__++)
             {
                 more[k__].interactable = false;
             }
         }
         else if (Gold_num + 800 > Int32.Parse(gold.text) && (k_ == 2 || k_ == 3))
         {
             for (int k__=2; k__<4;k__++)
             {
                 more[k__].interactable = false;
             }
             if (Gold_num + 250 > Int32.Parse(gold.text))
             {
                 for (int k__=0; k__<2;k__++)
                 {
                     more[k__].interactable = false;
                 }
             }
         }
         else if ((Gold_num + 250 > Int32.Parse(gold.text) && (k_==0 || k_==1)))
         {
             for (int k__=0; k__<4;k__++)
             {
                 more[k__].interactable = false;
             }
         }
         else
         {
             for (int k__=0; k__<4;k__++) {more[k__].interactable = true;}
         }
    }
    public void ImageChange(int id)
    {
        switch (id)
        {
            case 1:
            {
                _spriteNum++; 
                if (_spriteNum == 4) {_spriteNum = 0;}
                oldImage.sprite = newSprites[_spriteNum];
                break;
            }
            case 2:
            {
                _spriteNum--;
                if (_spriteNum == -1){ _spriteNum = 3;}
                oldImage.sprite = newSprites[_spriteNum];
                break;
            }
        }
    }
    public void Onbutton()
    {
        ImagePerson.image.sprite = oldImage2.sprite = oldImage.sprite;
        _newName = new [] {"имя: Морнэмир - «Черный алмаз»","имя: Арвен", "имя: Тамиорн", "имя: Таурэтари - «Королева леса»"};
        _newBiographi = new[] {"биография: Представитель одного из свободных народов Средиземья (эльфов), объединившихся для борьбы со Злом. Сын короля лунных эльфов, практически неуязвимый воин, заведший дружбу с Тамиорном."  ,"биография: Веселая, энергичная и беззаботная, и не производит впечатления бойца. Обладает довольно грозными физическими характеристиками - сила позволяет ей без видимых усилий скакать на десяток метров веерх, а уровень маны позволяет выдерживать неслабые магические атаки.", "биография: Глава дома, правитель северных регионов Семи Королевств (Хранитель Севера). Старый друг и боевой соратник правящего короля. Призван на службу в столицу в качестве нового Десницы после смерти (при таинственных обстоятельствах) предшественника на этом посту. Честен, смел и благонравен", "биография: Представитель одного из свободных народов Средиземья (эльфов), объединившихся для борьбы со Злом. Дочь главнокомандующего лесных эльфов, практически неуязвимый лучник."};
        oldName.text = _newName[_spriteNum];
        oldBiograpfi.text = _newBiographi[_spriteNum];
    }
}