using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterComun : MonoBehaviour
{

    public bool congelado = false; // esta variable servirá para que en los menús de selección no se muevan ni hagan nada

    public GameObject Point1, Point2, Point3, Point4;
    public LayerMask floorMask, wallMask;
    public bool onFloor = false, onWall = false, firstJump = false, secondJump = false, thirdJump = false;
    public bool player_1; // indica si es jugador 1 o no, de no serlo, será jugador 2
    public bool mirandoIzq, enMovimiento, saltando, puedeAtacar = false;
    public float spd, jumpSpd;
    private int totalJumps = 2;//<-------Variable para el total de saltos.
    public int currentJumps;//<----Variable para el total de saltos actual.

    [HideInInspector] public bool onAttack1 = false, onAttack2 = false;
    [HideInInspector] public int currentAttack;
    public float ContCurrentAttack;    

    public bool dead = false;

    public bool Damage1 = false, Damage2 = false;
    [HideInInspector] public float CountDamage;

    public Rigidbody2D Rigid;
    public Animator anim;

    public Vector3 targeted;

    private SoundManager SoundControl;

    public LandManager ManagerLand;
    public bool soundHit1;


    // Use this for initialization
    void Start()
    {

        Rigid = transform.GetComponent<Rigidbody2D>();

        SoundControl = GameObject.Find("SoundManager").GetComponent<SoundManager>();        

    }

    // Update is called once per frame
    void Update()
    {
        if (congelado == false)
        {
            DetectarZona();
            DetectarMovimientoyDireccion();
            Movimiento();
            GestionAnimaciones();
        }

        //esta parte debe estar en Update, porque si no no se activa el contador para parar las respectivas animaciones

        if (Damage1 == true)
        {
            anim.SetBool("damage", true);
            CountDamage += Time.deltaTime;
            if (CountDamage > 0.5f)
            {
                anim.SetBool("damage", false);
                Damage1 = false;
                ManagerLand.Damaged1 = false;
                CountDamage = 0;
            }
        }
        if(Damage2 == true)
        {
            anim.SetBool("damage", true);
            CountDamage += Time.deltaTime;
            if (CountDamage > 0.4f)
            {
                anim.SetBool("damage", false);
                Damage2 = false;
                ManagerLand.Damaged2 = false;
                CountDamage = 0;
            }
        }

        if (onAttack1 == true) //player[0] está atancando
        {
            if (ManagerLand.players[0].Pf_Player.name == "Player_1_Alda(Clone)")
            {
                if (ManagerLand.currentAttack == 1)
                {
                    
                    anim.SetBool("attack2", false);
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack1", true);
                    
                    if(soundHit1 == true)
                    {
                        SoundControl.CreateSound(4);
                        Destroy(GameObject.Find("Hand-1 (1)(Clone)"), 0.5f);

                        soundHit1 = false;
                    }
                    

                    ContCurrentAttack += Time.deltaTime;
                    if (ContCurrentAttack > 0.53f)
                    {
                        
                        anim.SetBool("attack1", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack1 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 2)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack2", true);
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(5);
                        soundHit1 = false;
                    }
                    ContCurrentAttack += Time.deltaTime;
                    if (ContCurrentAttack > 0.51f)
                    {
                        anim.SetBool("attack2", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack1 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 3)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack2", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack3", true);
                    if (soundHit1 == true)
                    {
                        
                        SoundControl.CreateSound(6);
                        soundHit1 = false;
                    }
                    ContCurrentAttack += Time.deltaTime;
                    if (ContCurrentAttack > 0.6f)
                    {
                        anim.SetBool("attack3", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack1 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 4)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack2", false);                    
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", true);
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(7);
                        soundHit1 = false;
                    }
                    ContCurrentAttack += Time.deltaTime;
                    if (ContCurrentAttack > 1f)
                    {
                        anim.SetBool("attackultimate", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack1 = false;
                    }
                }
            }
            else if (ManagerLand.players[0].Pf_Player.name == "Player_1_Sofi(Clone)")
            {
                if (ManagerLand.currentAttack == 1)
                {
                    anim.SetBool("attack2", false);
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack1", true);
                    ContCurrentAttack += Time.deltaTime;
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(8);
                        soundHit1 = false;
                    }
                    if (ContCurrentAttack > 0.4f)
                    {
                        anim.SetBool("attack1", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack1 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 2)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack2", true);
                    ContCurrentAttack += Time.deltaTime;
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(9);
                        soundHit1 = false;
                    }
                    if (ContCurrentAttack > 0.42f)
                    {
                        anim.SetBool("attack2", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack1 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 3)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack2", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack3", true);
                    ContCurrentAttack += Time.deltaTime;
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(10);
                        soundHit1 = false;
                    }
                    if (ContCurrentAttack > 0.53f)
                    {
                        anim.SetBool("attack3", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack1 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 4)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack2", false);
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", true);
                    ContCurrentAttack += Time.deltaTime;
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(3);
                        soundHit1 = false;
                    }
                    if (ContCurrentAttack > 1f)
                    {
                        anim.SetBool("attackultimate", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack1 = false;
                    }
                }
            }
        }
        else if (onAttack2 == true) //player[1] está atancando
        {
            if (ManagerLand.players[1].Pf_Player.name == "Player_1_Alda(Clone)")
            {
                if (ManagerLand.currentAttack == 1)
                {
                    anim.SetBool("attack2", false);
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack1", true);
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(4);
                        Destroy(GameObject.Find("Hand-1 (1)(Clone)"), 0.5f);
                        soundHit1 = false;
                    }

                    ContCurrentAttack += Time.deltaTime;
                    if (ContCurrentAttack > 0.53f)
                    {
                        anim.SetBool("attack1", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack2 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 2)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack2", true);
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(5);
                        soundHit1 = false;
                    }
                    ContCurrentAttack += Time.deltaTime;
                    if (ContCurrentAttack > 0.51f)
                    {
                        anim.SetBool("attack2", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack2 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 3)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack2", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack3", true);
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(6);
                        soundHit1 = false;
                    }
                    ContCurrentAttack += Time.deltaTime;
                    if (ContCurrentAttack > 0.3f)
                    {
                        anim.SetBool("attack3", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack2 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 4)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack2", false);
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", true);
                    if (soundHit1 == true)
                    {
                       SoundControl.CreateSound(7);
                        soundHit1 = false;
                    }
                    ContCurrentAttack += Time.deltaTime;
                    if (ContCurrentAttack > 3f)
                    {
                        
                        anim.SetBool("attackultimate", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack2 = false;
                    }
                }
            }
            else if (ManagerLand.players[1].Pf_Player.name == "Player_1_Sofi(Clone)")
            {
                if (ManagerLand.currentAttack == 1)
                {
                    anim.SetBool("attack2", false);
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack1", true);
                    ContCurrentAttack += Time.deltaTime;
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(8);
                        soundHit1 = false;
                    }
                    if (ContCurrentAttack > 0.4f)
                    {
                        anim.SetBool("attack1", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack2 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 2)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack2", true);
                    ContCurrentAttack += Time.deltaTime;
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(9);
                        soundHit1 = false;
                    }
                    if (ContCurrentAttack > 0.42f)
                    {
                        anim.SetBool("attack2", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack2 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 3)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack2", false);
                    anim.SetBool("attackultimate", false);
                    anim.SetBool("attack3", true);
                    ContCurrentAttack += Time.deltaTime;
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(10);
                        soundHit1 = false;
                    }
                    if (ContCurrentAttack > 0.53f)
                    {
                        anim.SetBool("attack3", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack2 = false;
                    }
                }
                else if (ManagerLand.currentAttack == 4)
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack2", false);
                    anim.SetBool("attack3", false);
                    anim.SetBool("attackultimate", true);
                    ContCurrentAttack += Time.deltaTime;
                    if (soundHit1 == true)
                    {
                        SoundControl.CreateSound(3);
                        soundHit1 = false;
                    }
                    if (ContCurrentAttack > 1f)
                    {
                        anim.SetBool("attackultimate", false);
                        soundHit1 = false;
                        ContCurrentAttack = 0;
                        onAttack2 = false;
                    }
                }
            }
        }
    }

    // función que detecta si el personajes está en el suelo, sujeto a un borde, pegado a una pared o el aire, todos los casos
    // en los que puede estar, y que tendrán diferentes comportamientos de cara al movimiento.
    public void DetectarZona()
    {
        onFloor = Physics2D.OverlapArea(Point1.transform.position, Point2.transform.position, floorMask);
        onWall = Physics2D.OverlapArea(Point3.transform.position, Point4.transform.position, wallMask);
    }


    public void DetectarMovimientoyDireccion()
    {
        if (player_1 == false)
        {
            if (Input.GetAxis("Horizontal2") > 0)
            {
                enMovimiento = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                mirandoIzq = false;
            }
            if (Input.GetAxis("Horizontal2") < 0)
            {
                enMovimiento = true;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                mirandoIzq = true;
            }
            if (Input.GetAxis("Horizontal2") == 0)
            {
                enMovimiento = false;
            }
        }
        else
        {
            if (Input.GetAxis("Horizontal1") > 0)
            {
                enMovimiento = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                mirandoIzq = false;
            }
            if (Input.GetAxis("Horizontal1") < 0)
            {
                enMovimiento = true;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                mirandoIzq = true;
            }
            if (Input.GetAxis("Horizontal1") == 0)
            {
                enMovimiento = false;
            }
        }
    }

    public void Movimiento()
    {

        if (player_1 == true)
        {
            //player1.transform.Translate(Vector2.right * Input.GetAxis("Horizontal1") * Speed * Time.deltaTime);

            // Al tener todos los tipos de personajes el script CharacterComun, se coge su velocidad con un simple getComponent
           
            if (Rigid.GetComponent<CharacterComun>().onFloor == true) //<--- le digo que solo salte si esta en el suelo , sino tendria saltos infinitos
            {
                saltando = false;
                firstJump = false;
                //DetectarMovimientoyDireccion(); --> no es necesario porque está ya llamada esa función en el Update
                currentJumps = 0;// al tocar el suelo se resetean los saltos 
                
                if (Input.GetKeyDown(KeyCode.W))
                {
                   
                    Rigid.AddForce(Vector2.up * transform.GetComponent<CharacterComun>().jumpSpd);
                   
                    
                    saltando = true;
                    firstJump = true;
                    
                }
                Rigid.velocity = new Vector2(transform.GetComponent<CharacterComun>().spd * Input.GetAxis("Horizontal1"), Rigid.velocity.y);// <-------para que no se enganche a las paredes
               


                
                

                    

                
            }

            if (Rigid.GetComponent<CharacterComun>().onFloor == false)//<--- aqui le digo que si no toca el suelo ni la pared tambien se pueda mover en horizontal.
            {
                if (Input.GetKeyDown(KeyCode.W) && currentJumps < totalJumps) //aqui añado el doble salto donde le digo que si el numero de saltos actuales es menor que el total de saltos pueda volver a saltar.
                {
                    Rigid.velocity = new Vector2(Rigid.velocity.x, 0);// <---- reseteo la velocidad de salto en cada salto.
                    Rigid.AddForce(Vector2.up * transform.GetComponent<CharacterComun>().jumpSpd); // linea del salto
                    currentJumps++;
                   
                    saltando = true;
                    secondJump = true;
                }
               if (Rigid.GetComponent<CharacterComun>().onWall == false)
               {
                    Rigid.velocity = new Vector2(transform.GetComponent<CharacterComun>().spd * Input.GetAxis("Horizontal1"), Rigid.velocity.y);// <-------para que no se enganche a las paredes
                    
                }
            }
        }
        else
        {
            //player1.transform.Translate(Vector2.right * Input.GetAxis("Horizontal1") * Speed * Time.deltaTime);

            // Al tener todos los tipos de personajes el script CharacterComun, se coge su velocidad con un simple getComponent

            if (Rigid.GetComponent<CharacterComun>().onFloor == true) //<--- le digo que solo salte si esta en el suelo , sino tendria saltos infinitos
            {
                saltando = false;
                firstJump = false;
                //DetectarMovimientoyDireccion(); --> no es necesario porque está ya llamada esa función en el Update
                currentJumps = 0;// al tocar el suelo se resetean los saltos 

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {

                    Rigid.AddForce(Vector2.up * transform.GetComponent<CharacterComun>().jumpSpd);


                    saltando = true;
                    firstJump = true;

                }
                Rigid.velocity = new Vector2(transform.GetComponent<CharacterComun>().spd * Input.GetAxis("Horizontal2"), Rigid.velocity.y);// <-------para que no se enganche a las paredes
                





            }

            if (Rigid.GetComponent<CharacterComun>().onFloor == false)//<--- aqui le digo que si no toca el suelo ni la pared tambien se pueda mover en horizontal.
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && currentJumps < totalJumps) //aqui añado el doble salto donde le digo que si el numero de saltos actuales es menor que el total de saltos pueda volver a saltar.
                {
                    Rigid.velocity = new Vector2(Rigid.velocity.x, 0);// <---- reseteo la velocidad de salto en cada salto.
                    Rigid.AddForce(Vector2.up * transform.GetComponent<CharacterComun>().jumpSpd); // linea del salto
                    currentJumps++;

                    saltando = true;
                    secondJump = true;
                }
                if (Rigid.GetComponent<CharacterComun>().onWall == false)
                {
                    Rigid.velocity = new Vector2(transform.GetComponent<CharacterComun>().spd * Input.GetAxis("Horizontal2"), Rigid.velocity.y);// <-------para que no se enganche a las paredes
                    
                }
            }

        }
    }

    public void GestionAnimaciones()
    {
        if(dead == true)
        {
            anim.SetBool("dead", true);
            congelado = true;
        }

        if (enMovimiento)
        {
            anim.SetBool("movimiento", true);
        }
        else
        {
            anim.SetBool("movimiento", false);
        }

        if (onFloor == true)
        {
            if (currentJumps == 0)
            {
                anim.SetBool("salto1", true);
                if (firstJump == true)
                {
                    SoundControl.CreateSound(0);
                    firstJump = false;
                    secondJump = true;
                }
            }
        }
        if (onFloor == false)
        { 
            if (currentJumps == 1)
            {
                
                anim.SetBool("salto1", false);
                anim.SetBool("salto2", true);
                
                if (secondJump == true)
                {
                    SoundControl.CreateSound(1);
                    secondJump = false;
                    thirdJump = true;
                }
            }

            if (currentJumps == 2)
            {
                anim.SetBool("salto2", false);
                anim.SetBool("salto3", true);

                if (thirdJump == true)
                {
                    SoundControl.CreateSound(2);
                    thirdJump = false;
                }
            }
        }
        else
        {
            anim.SetBool("salto1", false);
            anim.SetBool("salto2", false);
            anim.SetBool("salto3", false);
        }

        if (onFloor == true)
        {
            saltando = false;
            anim.SetBool("salto1", false);
            secondJump = false;
            thirdJump = false;
        }

        if (onFloor == true)
        {
            anim.SetBool("salto1", false);
        }

        if (onFloor == false && saltando == false)
        {
            anim.SetBool("salto1", true);
        }        
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "Player1" || Col.tag == "Player2")
        {
            puedeAtacar = true;
        }        
    }

    void OnTriggerStay2D(Collider2D Col)
    {
        if (Col.tag == "Player1" || Col.tag == "Player2")
        {
            puedeAtacar = true;
        }
    }

    void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.tag == "Player1" || Col.tag == "Player2")
        {
            puedeAtacar = false;
        }
    }
}