using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Land2Manager : MonoBehaviour
{

    [System.Serializable]
    public class PlayersProperties
    {
        public ManagerCharacter.CharacterProperties Character;
        public Vector2 PositionInstance;

        public GameObject Pf_Player;

        public int CurrentLife;
        public Image LifeBar;
        public int NumLifes = 3;
    }

    public List<PlayersProperties> players;

    public float Speed;
    public GameObject Cam;//<-------aqui pongo la camera para que pille a los players y haga el efecto foco

    private Rigidbody2D Rigid1;
    private Rigidbody2D Rigid2;

    public Text NumLifesText1, NumLifesText2; //para poner en pantalla las vidas que le quedan

    [HideInInspector] public int currentAttack; //para saber qué ataque se está produciendo en ese momento

    // Al inicializar se instancian los players en sus respectivas zonas de inicio y se guardan en variables del script para
    // poder manejarlos
    void Start()
    {        
        players[0].Character = GetComponent<ManagerCharacter>().GetCharacter(StaticScript.indicePlayer_1);
        players[0].Pf_Player = Instantiate(players[0].Character.Prefab, players[0].PositionInstance, Quaternion.Euler(0, 0, 0));
        players[0].CurrentLife = players[0].Character.Life;

        //GameObject newCharacter1 = (GameObject)Instantiate(tipoPersonajes[StaticScript.indicePlayer_1], new Vector2(-6.1f, 2.8f), Quaternion.Euler(0, 0, 0));
        //player1 = newCharacter1;

        // si hay que inicializar algo al crear el player que no tenga que ver con sus características únicas se hace desde aquí
        players[0].Pf_Player.tag = "Player1";
        players[0].Pf_Player.GetComponent<CharacterComun>().player_1 = true;

        Cam.GetComponent<MultipleTargetCamera>().targets.Add(players[0].Pf_Player.transform);//< ----para que la camera funcione y pille al player 1

        //aquí se le asigna su Rigid
        Rigid1 = players[0].Pf_Player.GetComponent<Rigidbody2D>();

        //aquí se le asigna su ID <----- Ya no va a hacer falta porque ya tienen sus IDs
        //if (player1 == newCharacter1)
        //{
        //ID_Mov.Add(0);
        //}
        
        players[1].Character = GetComponent<ManagerCharacter>().GetCharacter(StaticScript.indicePlayer_2);
        players[1].Pf_Player = Instantiate(players[1].Character.Prefab, players[1].PositionInstance, Quaternion.Euler(0, 180, 0));
        players[1].CurrentLife = players[1].Character.Life;

        //GameObject newCharacter2 = (GameObject)Instantiate(tipoPersonajes[StaticScript.indicePlayer_2], new Vector2(6.25f, 2.8f), Quaternion.Euler(0, 0, 0));
        //player2 = newCharacter2;

        // si hay que inicializar algo al crear el player que no tenga que ver con sus características únicas se hace desde aquí
        players[1].Pf_Player.tag = "Player2";
        players[1].Pf_Player.GetComponent<CharacterComun>().player_1 = false;

        Cam.GetComponent<MultipleTargetCamera>().targets.Add(players[1].Pf_Player.transform);//< ----para que la camera funcione y pille al player 2

        //aquí se le asigna su Rigidbody
        Rigid2 = players[1].Pf_Player.GetComponent<Rigidbody2D>();

        //aquí se le asigna su ID <----- Ya no va a hacer falta porque ya tienen sus IDs
        //if (player2 == newCharacter2)
        //{
        //ID_Mov.Add(1);
        //}



        //players[0].Pf_Player.GetComponent<CharacterComun>().ManagerL2 = GetComponent<Land2Manager>();
        //players[1].Pf_Player.GetComponent<CharacterComun>().ManagerL2 = GetComponent<Land2Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        //CharacterMovement(ID_Mov[0]);
        //CharacterMovement(ID_Mov[1]);

        Ataque();
        SistemaVidas();
        NumLifesText1.text = players[0].NumLifes.ToString();
        NumLifesText2.text = players[1].NumLifes.ToString();        
    }

    public void Ataque()
    {
       
        if (Input.GetKeyDown(KeyCode.F))
        {
            print("f");
            players[0].Pf_Player.GetComponent<CharacterComun>().onAttack1 = true; //Gestiono la animación desde CharacterComun
            players[0].Pf_Player.GetComponent<CharacterComun>().ContCurrentAttack = 0; //Lo pongo a cero para que no tenga que esperar a que se acabe la animacion anterior si pulso antes
            currentAttack = Random.Range(1, 4);
            if (players[0].Pf_Player.GetComponent<CharacterComun>().puedeAtacar == true)
            {
                players[1].CurrentLife -= RestarVida(0);
                players[1].LifeBar.fillAmount = ((float)players[1].CurrentLife / (float)players[1].Character.Life);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            players[1].Pf_Player.GetComponent<CharacterComun>().onAttack2 = true; //Gestiono la animación desde CharacterComun
            players[1].Pf_Player.GetComponent<CharacterComun>().ContCurrentAttack = 0; //Lo pongo a cero para que no tenga que esperar a que se acabe la animacion anterior si pulso antes
            currentAttack = Random.Range(1, 4);
            if (players[1].Pf_Player.GetComponent<CharacterComun>().puedeAtacar == true)
            {
                players[0].CurrentLife -= RestarVida(1);
                players[0].LifeBar.fillAmount = ((float)players[0].CurrentLife / (float)players[0].Character.Life);
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

    public void SistemaVidas()
    {
        if (players[0].Pf_Player.transform.position.y < -12f)
        {
            players[0].NumLifes--;
            players[0].Pf_Player.transform.position = new Vector2(players[0].PositionInstance.x, players[0].PositionInstance.y);
        }
        if (players[1].Pf_Player.transform.position.y < -12f)
        {
            players[1].NumLifes--;
            players[1].Pf_Player.transform.position = new Vector2(players[1].PositionInstance.x, players[1].PositionInstance.y);
        }

        if (players[0].CurrentLife <= 0)
        {
            players[0].NumLifes--;
            if (players[0].NumLifes == 0)
            {
                //Pondremos lo que sea para GAME OVER
            }
            else
            {
                players[0].CurrentLife = 100;
                players[0].LifeBar.fillAmount = ((float)players[0].CurrentLife / (float)players[0].Character.Life);
            }
        }
        else if (players[1].CurrentLife <= 0)
        {
            players[1].NumLifes--;
            if (players[1].NumLifes == 0)
            {
                //Pondremos lo que sea para GAME OVER
            }
            else
            {
                players[1].CurrentLife = 100;
                players[1].LifeBar.fillAmount = ((float)players[1].CurrentLife / (float)players[1].Character.Life);
            }
        }
    }

    //==============================================================================================================================================================
    //Funcion de movimiento para diferenciar sus controles dependiendo de qué player sean
    //public void CharacterMovement(int Id)
    //{
    //    if (Id == 0)
    //    {
    //        //player1.transform.Translate(Vector2.right * Input.GetAxis("Horizontal1") * Speed * Time.deltaTime);

    //        // Al tener todos los tipos de personajes el script CharacterComun, se coge su velocidad con un simple getComponent


    //        if (Rigid1.GetComponent<CharacterComun>().onFloor == true) //<--- le digo que solo salte si esta en el suelo , sino tendria saltos infinitos
    //        {
    //            if (Input.GetKeyDown(KeyCode.W))
    //            {
    //                Rigid1.AddForce(Vector2.up * player1.GetComponent<CharacterComun>().jumpSpd); // el 800 se sustituiría por la fuerza de salto del personaje
    //            }
    //            Rigid1.velocity = new Vector2(player1.GetComponent<CharacterComun>().spd * Input.GetAxis("Horizontal1"), Rigid1.velocity.y);// <-------para que no se enganche a las paredes
    //        }
    //        if (Rigid1.GetComponent<CharacterComun>().onFloor == false)//<--- aqui le digo que si no toca el suelo ni la pared tambien se pueda mover en horizontal.
    //        {
    //            if (Rigid1.GetComponent<CharacterComun>().onWall == false)
    //            {
    //                Rigid1.velocity = new Vector2(player1.GetComponent<CharacterComun>().spd * Input.GetAxis("Horizontal1"), Rigid1.velocity.y);// <-------para que no se enganche a las paredes
    //            }
    //        }

    //    }

    //    if (Id == 1)
    //    {
    //        //player2.transform.Translate(Vector2.right * Input.GetAxis("Horizontal2") * Speed * Time.deltaTime);

    //        // Al tener todos los tipos de personajes el script CharacterComun, se coge su velocidad con un simple getComponent


    //        if (Rigid2.GetComponent<CharacterComun>().onFloor == true)//<--- le digo que solo salte si esta en el suelo , sino tendria saltos infinitos
    //        {
    //            if (Input.GetKeyDown(KeyCode.UpArrow))
    //            {
    //                Rigid2.AddForce(Vector2.up * player2.GetComponent<CharacterComun>().jumpSpd);
    //            }
    //            Rigid2.velocity = new Vector2(player2.GetComponent<CharacterComun>().spd * Input.GetAxis("Horizontal2"), Rigid2.velocity.y);// <-------para que no se enganche a las paredes
    //        }
    //        if (Rigid2.GetComponent<CharacterComun>().onFloor == false)//<--- aqui le digo que si no toca el suelo ni la pared tambien se pueda mover en horizontal.
    //        {
    //            if (Rigid2.GetComponent<CharacterComun>().onWall == false)
    //            {
    //                Rigid2.velocity = new Vector2(player2.GetComponent<CharacterComun>().spd * Input.GetAxis("Horizontal2"), Rigid2.velocity.y);// <-------para que no se enganche a las paredes
    //            }
    //        }
    //    }
    //}
}