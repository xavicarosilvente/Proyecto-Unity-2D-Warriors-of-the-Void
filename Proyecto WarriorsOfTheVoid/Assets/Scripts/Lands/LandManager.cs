using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LandManager : MonoBehaviour
{
    public enum GameState { INIT, BATTLE, FINAL }
    public GameState State;

    [System.Serializable]
    public class PlayersProperties
    {
        public ManagerCharacter.CharacterProperties Character;
        public Vector3 PositionInstance;

        public GameObject Pf_Player;

        public int CurrentLife;
        public Image LifeBar;
        public int NumLifes = 3;
    }
    private SoundManager SoundControl;
    public AudioMenuScript ManagerAudio;

    public List<PlayersProperties> players;

    public GameObject Cam;//<-------aqui pongo la camera para que pille a los players y haga el efecto foco

    private Rigidbody2D Rigid1;
    private Rigidbody2D Rigid2;

    public Text CountdownText;
    private float Countdown = 3;

    public Text NumLifesText1, NumLifesText2; //para poner en pantalla las vidas que le quedan

    [HideInInspector] public int currentAttack; //para saber qué ataque se está produciendo en ese momento
    public GameObject AttackInfo;
    public bool Damaged1, Damaged2;    

    private int SpecialAttack1, SpecialAttack2;
    public Image SpecialAttackBar1, SpecialAttackBar2;

    public bool IsUltimateAttack;
    public bool specialatackforce;

    public GameObject AldaUlti;
    private Vector3 Ultitargeted;

    private Vector3 targeted;
    private float SpeedTarget = 11;
    public bool retroceso1, retroceso2;

    private int IndexWin; //para saber qué jugador gana
    public Text WinText;
    private float ContWinText;

    public CanvasGroup BotonesFinalBatalla;
    public float ContBotonesFinal;
    private float coolDown = 0.6f,coolDown2 = 0.6f;
    public float TimeStamp,TimeStamp2;

    // Al inicializar se instancian los players en sus respectivas zonas de inicio y se guardan en variables del script para
    // poder manejarlos
    void Start()
    {
        
        SoundControl = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        Destroy(GameObject.Find("AudioMenu"));

        players[0].Character = GetComponent<ManagerCharacter>().GetCharacter(StaticScript.indicePlayer_1);
        players[0].Pf_Player = Instantiate(players[0].Character.Prefab, players[0].PositionInstance, Quaternion.Euler(0, 0, 0));
        players[0].CurrentLife = players[0].Character.Life;

        // si hay que inicializar algo al crear el player que no tenga que ver con sus características únicas se hace desde aquí
        players[0].Pf_Player.tag = "Player1";
        players[0].Pf_Player.GetComponent<CharacterComun>().player_1 = true;

        Cam.GetComponent<MultipleTargetCamera>().targets.Add(players[0].Pf_Player.transform);//< ----para que la camera funcione y pille al player 1

        //aquí se le asigna su Rigid
        Rigid1 = players[0].Pf_Player.GetComponent<Rigidbody2D>();
        
        players[1].Character = GetComponent<ManagerCharacter>().GetCharacter(StaticScript.indicePlayer_2);
        players[1].Pf_Player = Instantiate(players[1].Character.Prefab, players[1].PositionInstance, Quaternion.Euler(0, 180, 0));
        players[1].CurrentLife = players[1].Character.Life;

        // si hay que inicializar algo al crear el player que no tenga que ver con sus características únicas se hace desde aquí
        players[1].Pf_Player.tag = "Player2";
        players[1].Pf_Player.GetComponent<CharacterComun>().player_1 = false;

        Cam.GetComponent<MultipleTargetCamera>().targets.Add(players[1].Pf_Player.transform);//< ----para que la camera funcione y pille al player 2

        //aquí se le asigna su Rigidbody
        Rigid2 = players[1].Pf_Player.GetComponent<Rigidbody2D>();        

        players[0].Pf_Player.GetComponent<CharacterComun>().ManagerLand = GetComponent<LandManager>();
        players[1].Pf_Player.GetComponent<CharacterComun>().ManagerLand = GetComponent<LandManager>();        

        BotonesFinalBatalla.alpha = 0;
        BotonesFinalBatalla.blocksRaycasts = false;
        BotonesFinalBatalla.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        switch (State)
        {
            case GameState.INIT:
                //Cuenta atrás antes de empezar
                players[0].Pf_Player.GetComponent<CharacterComun>().congelado = true;
                players[1].Pf_Player.GetComponent<CharacterComun>().congelado = true;
                CountdownText.enabled = true;
                Countdown -= Time.deltaTime;
                if (Countdown < 3 && Countdown > 2)
                {
                    CountdownText.text = "3 ...";
                }
                if (Countdown < 2 && Countdown > 1)
                {
                    CountdownText.text = "2 ...";
                }
                if (Countdown < 1 && Countdown > 0)
                {
                    CountdownText.text = "1 ...";
                }
                if (Countdown < 0 && Countdown > -1)
                {
                    CountdownText.text = "F i g h t !";
                }
                if (Countdown < -1)
                {
                    Countdown = 3;
                    CountdownText.enabled = false;
                    ChangeState(GameState.BATTLE);
                }
               

                break;

            case GameState.BATTLE:
                players[0].Pf_Player.GetComponent<CharacterComun>().congelado = false;
                players[1].Pf_Player.GetComponent<CharacterComun>().congelado = false;
                Ataque();
                SistemaVidas();
                NumLifesText1.text = players[0].NumLifes.ToString();
                NumLifesText2.text = players[1].NumLifes.ToString();
                SpecialAttackBar1.fillAmount = ((float)SpecialAttack1 / (float)100);
                SpecialAttackBar2.fillAmount = ((float)SpecialAttack2 / (float)100);
                
                break;

            case GameState.FINAL:
                WinText.enabled = true;
                if (IndexWin == 0) //gana el player 1
                {
                    players[1].Pf_Player.GetComponent<CharacterComun>().dead = true;
                    ContWinText += Time.deltaTime;
                    ContBotonesFinal += Time.deltaTime;
                    if (ContWinText > 1)
                    {
                        WinText.text = "Player 1 wins!";
                        ContWinText = 0;
                        
                        if (ContBotonesFinal > 2)
                        {
                            players[0].Pf_Player.GetComponent<CharacterComun>().congelado = true;
                            players[1].Pf_Player.GetComponent<CharacterComun>().congelado = true;
                            BotonesFinalBatalla.alpha = 1;
                            BotonesFinalBatalla.blocksRaycasts = true;
                            BotonesFinalBatalla.interactable = true;
                        }
                    }
                }
                else //gana el player 2
                {
                    players[0].Pf_Player.GetComponent<CharacterComun>().dead = true;
                    ContWinText += Time.deltaTime;
                    ContBotonesFinal += Time.deltaTime;
                    if (ContWinText > 1)
                    {
                        WinText.text = "Player 2 wins!";
                        ContWinText = 0;
                    
                        if (ContBotonesFinal > 2)
                        {
                            players[0].Pf_Player.GetComponent<CharacterComun>().congelado = true;
                            players[1].Pf_Player.GetComponent<CharacterComun>().congelado = true;
                            BotonesFinalBatalla.alpha = 1;
                            BotonesFinalBatalla.blocksRaycasts = true;
                            BotonesFinalBatalla.interactable = true;
                        }
                    }
                }
                break;
        }
    }

    public void Ataque()
    {
        TimeStamp += Time.deltaTime;
        if (TimeStamp >= coolDown)
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                IsUltimateAttack = false;

                players[0].Pf_Player.GetComponent<CharacterComun>().soundHit1 = true;
                
                if (Damaged1 == false)
                {
                    players[0].Pf_Player.GetComponent<CharacterComun>().onAttack1 = true; //Gestiono la animación desde CharacterComun
                    players[0].Pf_Player.GetComponent<CharacterComun>().ContCurrentAttack = 0; //Lo pongo a cero para que no tenga que esperar a que se acabe la animacion anterior si pulso antes
                    currentAttack = Random.Range(1, 4);

                    if (Input.GetAxis("Horizontal1") > 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    if (Input.GetAxis("Horizontal1") < 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }

                    if (players[0].Pf_Player.GetComponent<CharacterComun>().puedeAtacar == true)
                    {
                        int TempDamage = RestarVida(0);
                        players[1].CurrentLife -= TempDamage;
                        players[1].LifeBar.fillAmount = ((float)players[1].CurrentLife / (float)players[1].Character.Life);
                        Damaged2 = true;
                        players[1].Pf_Player.GetComponent<CharacterComun>().Damage2 = true;
                        retroceso2 = true;

                        if (TempDamage == 0)
                        {
                            GameObject NewAttackInfo = Instantiate(AttackInfo, new Vector3(players[1].LifeBar.transform.position.x, players[1].LifeBar.transform.position.y, 90),
                                Quaternion.identity);
                            NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "0";
                            NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.green;
                            Destroy(NewAttackInfo, 1);
                        }
                        else
                        {
                            GameObject NewAttackInfo = Instantiate(AttackInfo, new Vector3(players[1].LifeBar.transform.position.x, players[1].LifeBar.transform.position.y, 90),
                                Quaternion.identity);
                            NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "-" + TempDamage;
                            NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.yellow;
                            Destroy(NewAttackInfo, 1);
                            SpecialAttack1 += 10;
                            if (SpecialAttack1 > 100)
                            {
                                SpecialAttack1 = 100;
                            }
                        }
                    }
                }

                TimeStamp = 0;
            }

            
        }
        

        if (Damaged2 == false)
        {
            
            TimeStamp2 += Time.deltaTime;
            if (TimeStamp2 >= coolDown2)
            {
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    players[1].Pf_Player.GetComponent<CharacterComun>().soundHit1 = true;
                    IsUltimateAttack = false;
                    players[1].Pf_Player.GetComponent<CharacterComun>().onAttack2 = true; //Gestiono la animación desde CharacterComun
                    players[1].Pf_Player.GetComponent<CharacterComun>().ContCurrentAttack = 0; //Lo pongo a cero para que no tenga que esperar a que se acabe la animacion anterior si pulso antes
                    currentAttack = Random.Range(1, 4);

                    if (Input.GetAxis("Horizontal2") > 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    if (Input.GetAxis("Horizontal2") < 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }

                    if (players[1].Pf_Player.GetComponent<CharacterComun>().puedeAtacar == true)
                    {
                        int TempDamage = RestarVida(1);
                        players[0].CurrentLife -= TempDamage;
                        players[0].LifeBar.fillAmount = ((float)players[0].CurrentLife / (float)players[0].Character.Life);
                        Damaged1 = true;
                        players[0].Pf_Player.GetComponent<CharacterComun>().Damage1 = true;
                        retroceso1 = true;

                        if (TempDamage == 0)
                        {
                            GameObject NewAttackInfo = Instantiate(AttackInfo, new Vector3(players[0].LifeBar.transform.position.x, players[0].LifeBar.transform.position.y, 90),
                                Quaternion.identity);
                            NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "0";
                            NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.green;
                            Destroy(NewAttackInfo, 1);
                        }
                        else
                        {
                            GameObject NewAttackInfo = Instantiate(AttackInfo, new Vector3(players[0].LifeBar.transform.position.x, players[0].LifeBar.transform.position.y, 90),
                                Quaternion.identity);
                            NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "-" + TempDamage;
                            NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.yellow;
                            Destroy(NewAttackInfo, 1);
                            SpecialAttack2 += 10;
                            if (SpecialAttack2 > 100)
                            {
                                SpecialAttack2 = 100;
                            }
                        }
                    }
                    TimeStamp2 = 0;
                }
            }
        }

        //Ataque ultimate del P1
        if (Input.GetKeyDown(KeyCode.C) && SpecialAttack1 == 100) 
        {
            players[0].Pf_Player.GetComponent<CharacterComun>().soundHit1 = true;
            IsUltimateAttack = true;
            SpecialAttack1 = 0;
            if(players[0].Character.ID == 1)
            {
                SoundControl.CreateSound(3);
            }
            if (players[0].Character.ID == 0)
            {
                
                if (players[0].Pf_Player.transform.position.x < players[1].Pf_Player.transform.position.x)
                {
                    Ultitargeted = new Vector3(players[0].Pf_Player.transform.position.x + 3.3f, players[0].Pf_Player.transform.position.y, players[0].Pf_Player.transform.position.z);
                    GameObject NewUlti = Instantiate(AldaUlti, Ultitargeted, Quaternion.Euler(0, 0, 0));
                    NewUlti.GetComponent<UltimateRayos>().ManagerLand = GetComponent<LandManager>();
                    NewUlti.GetComponent<UltimateRayos>().Rayos = true;
                }
                if (players[0].Pf_Player.transform.position.x > players[1].Pf_Player.transform.position.x)
                {
                    Ultitargeted = new Vector3(players[0].Pf_Player.transform.position.x - 3.3f, players[0].Pf_Player.transform.position.y, players[0].Pf_Player.transform.position.z);
                    GameObject NewUlti = Instantiate(AldaUlti, Ultitargeted, Quaternion.Euler(0, 180, 0));
                    NewUlti.GetComponent<UltimateRayos>().ManagerLand = GetComponent<LandManager>();
                    NewUlti.GetComponent<UltimateRayos>().Rayos = true;
                }
            } 
            players[0].Pf_Player.GetComponent<CharacterComun>().onAttack1 = true; //Gestiono la animación desde CharacterComun
            currentAttack = 4;

            if (Input.GetAxis("Horizontal1") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetAxis("Horizontal1") < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (players[0].Pf_Player.GetComponent<CharacterComun>().puedeAtacar == true)
            {
                int TempDamage = RestarVidaSpecial(0);
                players[1].CurrentLife -= TempDamage;
                players[1].LifeBar.fillAmount = ((float)players[1].CurrentLife / (float)players[1].Character.Life);
                Damaged2 = true;
                players[1].Pf_Player.GetComponent<CharacterComun>().Damage2 = true;
                retroceso2 = true;

                GameObject NewAttackInfo = Instantiate(AttackInfo, new Vector3(players[1].LifeBar.transform.position.x, players[1].LifeBar.transform.position.y, 90),
                        Quaternion.identity);
                NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "-" + TempDamage;
                NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.red;
                Destroy(NewAttackInfo, 1);
            }
        }
        //Ataque ultimate del P2
        if (Input.GetKeyDown(KeyCode.RightControl) && SpecialAttack2 == 100)
        {
            players[1].Pf_Player.GetComponent<CharacterComun>().soundHit1 = true;
            IsUltimateAttack = true;
            SpecialAttack2 = 0;
            if (players[0].Character.ID == 1)
            {
                SoundControl.CreateSound(3);
            }
            if (players[1].Character.ID == 0)
            {
                if (players[1].Pf_Player.transform.position.x < players[0].Pf_Player.transform.position.x)
                {
                    Ultitargeted = new Vector3(players[1].Pf_Player.transform.position.x + 3.3f, players[1].Pf_Player.transform.position.y, players[1].Pf_Player.transform.position.z);
                    GameObject NewUlti = Instantiate(AldaUlti, Ultitargeted, Quaternion.Euler(0, 0, 0));
                    NewUlti.GetComponent<UltimateRayos>().ManagerLand = GetComponent<LandManager>();
                    NewUlti.GetComponent<UltimateRayos>().Rayos = true;
                }
                if (players[1].Pf_Player.transform.position.x > players[0].Pf_Player.transform.position.x)
                {
                    Ultitargeted = new Vector3(players[1].Pf_Player.transform.position.x - 3.3f, players[1].Pf_Player.transform.position.y, players[1].Pf_Player.transform.position.z);
                    GameObject NewUlti = Instantiate(AldaUlti, Ultitargeted, Quaternion.Euler(0, 180, 0));
                    NewUlti.GetComponent<UltimateRayos>().ManagerLand = GetComponent<LandManager>();
                    NewUlti.GetComponent<UltimateRayos>().Rayos = true;
                }
            }
            players[1].Pf_Player.GetComponent<CharacterComun>().onAttack2 = true; //Gestiono la animación desde CharacterComun
            currentAttack = 4;

            if (Input.GetAxis("Horizontal1") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetAxis("Horizontal1") < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (players[1].Pf_Player.GetComponent<CharacterComun>().puedeAtacar == true)
            {
                int TempDamage = RestarVidaSpecial(1);
                players[0].CurrentLife -= TempDamage;
                players[0].LifeBar.fillAmount = ((float)players[0].CurrentLife / (float)players[0].Character.Life);
                Damaged1 = true;
                players[0].Pf_Player.GetComponent<CharacterComun>().Damage1 = true;
                retroceso1 = true;

                GameObject NewAttackInfo = Instantiate(AttackInfo, new Vector3(players[0].LifeBar.transform.position.x, players[0].LifeBar.transform.position.y, 90),
                        Quaternion.identity);
                NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "-" + TempDamage;
                NewAttackInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.red;
                Destroy(NewAttackInfo, 1);
            }
        }

        //retrocesos tras recibir un ataque
        if (retroceso1 == true)
        {
            if (IsUltimateAttack != true) //No es un ataque ultimate
            {
                //Comprobamos si el player 1 (que recibe el golpe) está a la derecha o a la izquierda del player 2
                if (players[0].Pf_Player.transform.position.x > players[1].Pf_Player.transform.position.x)
                {
                    //Marcamos el objetivo donde va a llegar
                    targeted = new Vector3(players[0].Pf_Player.transform.position.x + Random.Range(0.2f, 0.6f), players[0].Pf_Player.transform.position.y,
                        players[0].Pf_Player.transform.position.z);

                    //Linea del retroceso en si
                    players[0].Pf_Player.transform.position = Vector3.Lerp(players[0].Pf_Player.transform.position, targeted, SpeedTarget * Time.deltaTime);

                    //Desactivamos retroceso
                    if (Damaged1 == false)
                    {
                        retroceso1 = false;
                    }
                }
                else if (players[0].Pf_Player.transform.position.x < players[1].Pf_Player.transform.position.x)
                {
                    targeted = new Vector3(players[0].Pf_Player.transform.position.x - Random.Range(0.2f, 0.6f), players[0].Pf_Player.transform.position.y,
                        players[0].Pf_Player.transform.position.z);
                    players[0].Pf_Player.transform.position = Vector3.Lerp(players[0].Pf_Player.transform.position, targeted, SpeedTarget * Time.deltaTime);
                    if (Damaged1 == false)
                    {
                        retroceso1 = false;
                    }
                }
            }
            else //Es un ataque ultimate
            {
                if (players[0].Pf_Player.transform.position.x > players[1].Pf_Player.transform.position.x)
                {
                    specialatackforce = true;
                    if(specialatackforce ==true)
                    {
                        Rigid1.AddForce(new Vector2(Random.Range(500, 600), Random.Range(17, 19)));
                        
                    }
                    if (Damaged1 == false)
                    {
                        retroceso1 = false;
                        IsUltimateAttack = false;
                    }
                }
                else if (players[0].Pf_Player.transform.position.x < players[1].Pf_Player.transform.position.x)
                {
                    specialatackforce = true;
                    if (specialatackforce == true)
                    {
                        Rigid1.AddForce(new Vector2(Random.Range(-500, -600), Random.Range(17, 19)));

                    }
                    if (Damaged1 == false)
                    {
                        retroceso1 = false;
                        IsUltimateAttack = false;
                    }
                }
            }
        }

        if (retroceso2 == true)
        {
            if (IsUltimateAttack != true) //No es un ataque ultimate
            {
                if (players[1].Pf_Player.transform.position.x > players[0].Pf_Player.transform.position.x)
                {
                    targeted = new Vector3(players[1].Pf_Player.transform.position.x + Random.Range(0.2f, 0.6f), players[1].Pf_Player.transform.position.y,
                        players[1].Pf_Player.transform.position.z);
                    players[1].Pf_Player.transform.position = Vector3.Lerp(players[1].Pf_Player.transform.position, targeted, SpeedTarget * Time.deltaTime);
                    if (Damaged2 == false)
                    {
                        retroceso2 = false;
                    }
                }
                else if (players[1].Pf_Player.transform.position.x < players[0].Pf_Player.transform.position.x)
                {
                    targeted = new Vector3(players[1].Pf_Player.transform.position.x - Random.Range(0.2f, 0.6f), players[1].Pf_Player.transform.position.y,
                        players[1].Pf_Player.transform.position.z);
                    players[1].Pf_Player.transform.position = Vector3.Lerp(players[1].Pf_Player.transform.position, targeted, SpeedTarget * Time.deltaTime);
                    if (Damaged2 == false)
                    {
                        retroceso2 = false;
                    }
                }
            }
            else //Es un ataque ultimate
            {
                if (players[1].Pf_Player.transform.position.x > players[0].Pf_Player.transform.position.x)
                {

                    specialatackforce = true;

                    if(specialatackforce == true)
                    {
                        Rigid2.AddForce(new Vector2(Random.Range(500,600),Random.Range(17,19)));
                       
                    }
                    if (Damaged2 == false)
                    {
                        retroceso2 = false;
                        IsUltimateAttack = false;
                    }
                }
                else if (players[1].Pf_Player.transform.position.x < players[0].Pf_Player.transform.position.x)
                {
                    specialatackforce = true;

                    if (specialatackforce == true)
                    {
                        Rigid2.AddForce(new Vector2(Random.Range(-500, -600), Random.Range(17, 19)));
                        
                    }
                    if (Damaged2 == false)
                    {
                        retroceso2 = false;
                        IsUltimateAttack = false;
                    }
                }
            }
        }
    }

    public int RestarVida(int ID)
    {
        //Calculamos el valor del ataque
        int WeaponAttack = 0;
        WeaponAttack = Random.Range((int)players[ID].Character.Attack.x, (int)players[ID].Character.Attack.y);

        //Esto solo es para saber quién pega a quién. Si el ID (quien pega) es 0, entonces el RivalID es 1, y viceversa.
        int RivalID = 0;
        if (ID == 0)
        {
            RivalID = 1;
        }

        //Calculamos el valor de la defensa
        int WeaponDefense = 0;
        WeaponDefense = Random.Range((int)players[RivalID].Character.Defense.x, (int)players[RivalID].Character.Defense.y);

        //Hacemos la resta para sacar el valor del daño final realizado
        int TotalAttackValue = WeaponAttack - WeaponDefense;
        if (TotalAttackValue < 0)
        {
            TotalAttackValue = 0;
        }

        return TotalAttackValue;        
    }

    public int RestarVidaSpecial(int ID)
    {
        int WeaponAttack = 0;
        WeaponAttack = Random.Range((int)players[ID].Character.Attack.x, (int)players[ID].Character.Attack.y);

        int RivalID = 0;
        if (ID == 0)
        {
            RivalID = 1;
        }

        int WeaponDefense = 0;
        WeaponDefense = Random.Range((int)players[RivalID].Character.Defense.x, (int)players[RivalID].Character.Defense.y);

       int TotalAttackValue = (WeaponAttack * 3) - WeaponDefense;
        if (TotalAttackValue < 0)
        {
            TotalAttackValue = 0;
        }

        return TotalAttackValue;
    }

    public void SistemaVidas()
    {
        if (players[0].Pf_Player.transform.position.y < -12f)
        {
            players[0].CurrentLife = 0;
            players[0].Pf_Player.transform.position = new Vector2(players[0].PositionInstance.x, players[0].PositionInstance.y);
        }
        if (players[1].Pf_Player.transform.position.y < -12f)
        {
            players[1].CurrentLife = 0;
            players[1].Pf_Player.transform.position = new Vector2(players[1].PositionInstance.x, players[1].PositionInstance.y);
        }

        if (players[0].CurrentLife <= 0)
        {
            if (players[0].NumLifes > 0)
            {
                players[0].NumLifes--;
                if (players[0].NumLifes != 0)
                {
                    players[0].CurrentLife = 100;
                    players[0].LifeBar.fillAmount = ((float)players[0].CurrentLife / (float)players[0].Character.Life);
                }
            }
            else if (players[0].NumLifes == 0)
            {
                IndexWin = 1;
                ChangeState(GameState.FINAL);
            }
        }
        else if (players[1].CurrentLife <= 0)
        {
            if (players[1].NumLifes > 0)
            {
                players[1].NumLifes--;
                if (players[1].NumLifes != 0)
                {
                    players[1].CurrentLife = 100;
                    players[1].LifeBar.fillAmount = ((float)players[1].CurrentLife / (float)players[1].Character.Life);
                }
            }
            else if (players[1].NumLifes == 0)
            {
                IndexWin = 0;
                ChangeState(GameState.FINAL);
            }
        }
    }

    private void ChangeState(GameState NewState)
    {
        State = NewState;
    }


    //botones final de batalla
    public void MainMenuButton()
    {
        SceneManager.LoadScene(1);
    }
    public void SelectCharactersButton()
    {
        SceneManager.LoadScene(2);
    }
    
}