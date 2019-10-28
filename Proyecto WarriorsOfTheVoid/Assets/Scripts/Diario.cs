/*
 *<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Xavi 14/6/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto:Creación Script Camera
 * 
 * He creado el script de la camera al estilo Smash Bross que tiene el nombre de " MultipleTarget Camera".
 * Constiste basicamente en que la camera siga el punto medio de los players en escena, si uno se aleja la camara
 * alejara el zoom si estos se acercan la camara ampliara el zoom, el script estara puesto en la camara de la escena de la partida, 
 * que hemos llamado Lands"escenario de juego"
 * En este script veres que he creado una lista llamada Targets, aquí se introducen los prefabs de los players desde el inspector para
 * que la camera sepa  a quien a de seguir.
 *---------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 *<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Suri 14/6/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto 1: Creación Script menú de selección de personajes
 * 
 * He creado el script que maneja el menú de selección de personajes, tiene el nombre de "MenuSelectChar"
 * Está creado para que el menú de personajes funcione de la siguiente manera:
 * Empezamos eligiendo el personaje para el player 1.
 * Tenemos los botones de los personajes (los de la parte superior). Cuando pulsas uno de esos botones, el personaje seleccionado
 * se muestra por pantalla en la parte del player correspondiente.
 * Para pasar de seleccionar el plyer 1 a seleccionar el player 2 tendremos que pulsar el botón que hay debajo del player 1. En
 * caso de no haber seleccionado personaje, el botón no hará nada.
 * Pasamos a elegir el personaje para el player 2.
 * Una vez seleccionado y confirmado, aparecen dos nuevos botones, uno para volver a elegir los personajes y otro para pasar
 * al menú de selección de escenarios.
 * 
 *
 * Asunto 2: Creación Script menú de selección de escenarios
 * 
 * Este script está muy a medias, tiene el nombre de "SelectLandManager"
 * Por ahora sólo está implementado que cada botón de los diferentes escenarios te lleve a la escena correspondiente al escenario
 * seleccionado y que el botón de confirmación haga el cambio de escena.
 * 
 * Asunto 3: Creación Script de variables estáticas
 * 
 * He creado un script para manejar variables estáticas, tiene el nombre de "StaticScript"
 * Por ahora sólo veo dos variables estáticas estrictamente necesarias, que son qué personaje se eligió en el menú de selección de
 * personajes.
 * Cuantas menos estáticas haya mejor, pero si se necesitan o facilitan el trabajo de alguna manera se pueden usar sin problemas.
 * 
 *---------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Suri 15/6/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto 1: Creación Scripts de las Lands
 * 
 * He pensado que es mejor que cada Land tenga su propio script manager en vez de uno común, ya que no solo los personajes se 
 * instancian en coordenadas diferentes según la Land, si no que de haber elementos interactuables (como por ejemplo que sople 
 * una ráfaga de aire de vez en cuando que desplace a los player etc, se controlará mejor desde un mismo script, a mi parecer 
 * claro)
 * 
 * Por ahora solo se instancian los personajes adecuados en las lands, dejo el terreno preparado para que Sergio lo continue.
 * 
 * Tal vez parezca aparatoso lo de usar un script para cada land si acabamos usando 30 escenarios, ya que serán scripts bastante
 * parecidos, pero a priori es la opción más versatil que veo de cara a añadir el toque único al land.
 * Se puede discutir pero como vamos a trabajar dividiendo todo en funciones, una vez haya una función se puede copiar en el 
 * resto de scripts.
 * 
 * Asunto 2: Pensar como manejar los players
 * 
 * He pensado que si el script manager de las lands ya tiene a los propios players como gameObjects se puede hacer todo lo 
 * básico desde ahí, el movimiento, los ataques básicos, etc.
 * Y de cara a hacer los players únicos, sería ponerle un script al prefab para incluir las funciones únicas como habilidades etc.
 * 
 * Asunto 3: Otros
 * 
 * He añadido unas físicas base en las escenas que las necesitaban
 * He ajustado un poco el menú de seleccion de personajes para mejorar un poco lo visual. (creo)
 * 
 * ---------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Nathan 18/6/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Asignacion de teclas por player + Diferenciacion de players
 *  
 * Primero me fui a los Project Settings -> Inputs 
 * y como aparecen 2 juegos de Axis cambié uno de ellos por Horizontal2 y Vertical2,
 * en el Horizontal1 y Vertical1 permite mover con las flechas y con a w s d así que le quito la posibilidad de usar las flechas
 * y se las asigno al Horizontal2 y Vertical2, también cambio lo demás para que quede exactamente igual al 1.
 * 
 * Dentro ya del Script:
 * 
 * Como se propuso asigné el movimiento de los players dentro de los scripts de Land, empecé por el Land1.
 * 
 * Creo una Lista de IDs para poder guardar los dos IDs de los personajes en un mismo sitio, 
 * después nada más instanciar los personajes añado su ID a la lista (como si les hubiera asignado un ID)
 * y creo la función para mover los personajes:
 * 
 * Si es el ID 0 usa el conjunto de Axis1 y si es el Id 1 usa el conjunto de Axis2
 * 
 * Añado la función en el Update con sus IDs correspondientes.
 * 
 * Usar a/w para Player1 y flechas derecha/izquierda Player2
 * 
 * Lo mismo hecho en el Land2.
 * 
 * Ahora mismo como tenemos 2 Lands podemos hacerlo así pero creo que lo mejor sería crear un PlayerManager
 * en el cuál al instanciar los Players en los Lands, los pase al PlayerManager y así tener uno para los Player
 * y otro para los Lands. 
 * Ésta parte si estamos todos de acuerdo y se necesita hacer más adelante, podría
 * hacerla yo sin problema.
 * 
 * ---------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Xavi 18/06/2018   20:49h>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto : Ajuste camera en las lands
 * 
 * He añadido un GameObject llamado Cam en el script de Land1Manager y Land2Manager donde en este asigno la camera para poder acceder a los players,
 * justo debajo de donde se instancia los personajes en el Land1Manager y Land2Manager vereis que he añadido unas lineas como estas...
 * 
 * " Cam.GetComponent<MultipleTargetCamera>().targets.Add(player1.transform);"  <------es para poder acceder a la lista de la camera que controla el foco y
 * me coja los players que seleccionamos en el menu de personajes.
 * 
 * Ademas he congelado en los prefabs de los players en el rigidbody la Z, paa que estos no giren al saltar o al bajar un desnivel.
 * 
 * ------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 *<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Nathan 19/6/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 *
 * Asunto: Arreglo Rigidbody
 * 
 * Como había un problema de vibración he usado el Rigidbody para solucionarlo, ambos Lands hechos.
 * 
 * También como en los Lands ya tenemos los personajes instanciados con su número de player (lo que hice el otro día), 
 * es mucho más sencillo a la hora de trabajar que diferenciar qué player son desde un script aparte.
 * 
 * He creado las variables Rigid1 y Rigid2, después tras instanciar los personajes se les asigna a cada variable el componente Rigidbody del player correspondiente.
 * 
 * Después en la función de CharacterMovement() he cambiado la linea de movimiento (y dejado con // la antigua por si alguien queria verla)
 * para usar el Rigid.velocity que es lo que nos permite que no haya fallo de vibracion.
 * 
 * ------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<<<<<<<<<<<<Suri 20/6/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto 1: gestión de las máscaras de suelo y paredes
 * 
 * He creado 4 puntos en los prefabs para detectar los suelos y las paredes.
 * De esos 4 puntos he creado 3 máscaras.
 * Una para detectar el suelo con los puntos 1 y 2, otra para detectar paredes con los puntos 3 y 4, y otra para detectar bordes
 * con los puntos 3 y 4. 
 * 
 * 
 * Asunto 2: comienzo del movimiento detallado de los personajes
 * 
 * Una vez hechas las máscaras para detectar donde está el personaje, he hecho una función para saber si el 
 * personaje está en el aire, está simplemente en el suelo, simplemente pegado a una pared, o si está sujeto en un borde.
 * Esto es de cara a más adelante, cuando haya que detallar el comportamiento de cómo se mueve el personaje, que ataques hará 
 * si está en el aire o en el suelo, etc.
 * 
 * Asunto 3: Saltos (POR AHORA SOLO POR PROBAR)
 * 
 * Para comprobar que se detectaban bien todas máscaras y se notaba la superficie en la que estaba el personaje (aire, suelo,
 * pegado a una pared, agarrado a un borde), he asignado teclas para saltar que ya se verá más adelante donde acoplar.
 * ESTO ERA SOLO DE PRUEBA, pero lo dejo igualmente porque sirve de ejemplo.
 * 
 * 
 * Asunto 4: comienzo del control de los personajes en 2 scripts
 * 
 * previa consulta con Xavi, he pensado en la forma que a mi parecer va a ser la más comoda para trabajar con los personajes.
 * Los personajes tendrán todos 2 scripts, uno llamado "CharacterComun", que son las caracteristicas que tienen todos los personajes 
 * en común (fuerza de salto, indice de daño, fuerza, velocidad, etc.) y un segundo sript con el nombre del personaje (por ejemplo 
 * "SrMono") que será el script que tendrá las funciones únicas del personaje e inicializará el script "CharacterComun" para 
 * asignar de forma única las características del personaje.
 * 
 * Por el momento he puesto poquito para que veais como funciona.
 * ahora los dos personajes tienen diferente velocidad y diferente potencia de salto.
 * Más adelante habrá que meter daño sufrido, fuerza, etc.
 * 
 * ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Xavi 20/06/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 *
 * Asunto: Ajuste del Salto y engancharse en paredes
 * 
 * 
 *Lo que he echo es en el script Land1Manager y Land2Manager decirle que si el onFloor es true pueda saltar si no es asi no podra,
 * tambien he ajustado los puntos de los personajes para que las conexiones con la pred y suelo sea correcto ademas de añadir correctamente a todo las paredes en el land 1 y 2.
 * 
 * 
 * Más adelante se le puede añadir el doble salto y tambien que se pueda saltar cuando toque la pared 
 * para que pueda intentar volver a subir a la plataforma en el caso de caerse, es una propuesta.
 * 
 *---------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
 * 
 * <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Suri 21/6/2018>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Forma de identificar desde el propio personaje que player es y Reorganización de funciones
 * 
 * Dado que en un futuro se va a necesitar saber si el personaje es player 1 o 2 para activar sus habilidades especiales con un 
 * botón u otro, y eso se hace desde el script de personaje único, he hecho que desde el script comun a todos los personajes,
 * mediante una bool se identifique si es player 1 o 2.
 * 
 * Aprovechando eso he reorganizado la función de movimiento, que pasa de estar en los scripts de las lands a estar sólo en el
 * script que todos los personajes tienen en común, dejando así más limpio el codigo de las lands.
 * 
 * -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 *<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Xavi 24/06/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Añadir el triple salto
 * 
 * En el script de CharacterComun he añadido el triple salto del player 1 y 2, he creado 2 variables int nuevas la de currentJumps y la de totalJumps,
 * luego le digo que cuando el onFloor sea falso y que si el valor de currentJumps es menor que el totalJumps pueda volver a saltar...
 * 
 * añado el Currentjumps++; para que vaya sumando saltos y cuando toque el suelo este resetee el contador currentJumps = 0 de esta manera solo podra saltar
 * las veces que pongamos en el totaljumps.
 * 
 * Ademas añado esta linea " Rigid.velocity = new Vector2(Rigid.velocity.x, 0);" que sirve que cada vez que salte mi velocidad de Y siempre 
 * sea la misma asi todos los saltos tienen la misma fuerza.
 * 
 * -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Suri 25/06/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto 1: creación de la variable "congelado" en el script CharacterComun
 * 
 * Al cambiar el control de los personajes desde el script de las lands al script CharacterComun, en los menús de selección
 * era posible mover al personaje y hacer que desapareciera y pasaran cosas raras.
 * He creado una bool llamada "congelado" para que en esas escenas no hagan nada más que estar quietecicos en el idle los
 * personajes.
 * 
 * Asunto 2: creación función DetectarDireccion en el script CharacterComun
 * 
 * Para crear la función he añadido una nueva bool, "mirandoIzq" que como su nombre indica marcará si el personaje mira a la
 * izquierda (true) o en caso contrario mira a la derecha (false).
 * Esa variable aún no sé si va a ser necesaria al final ************
 * 
 * En la función básicamente hace girar 180 grados en el eje y el quad según que tecla presiones.
 * 
 * Asunto 3: creación función DetectarMovimiento en el script CharacterComun
 * 
 * Para esta función he creado una nueva variable bool, "enMovimiento", que indica exactamente lo que dice su nombre.
 * Esta variable servirá para controlar las animaciones, para pasar de idle a run, "enMovimiento" deberá estar en true, y para
 * pasar de run a idle, tendrá que estar en false.
 * 
 * Asunto 4: Creación función GestionAnimaciones en el script CharacterComun
 * 
 * Por ahora solo se gestiona el idle y el run, pero la idea es que en esta función mediante bool previamente creadas, como hago 
 * con estas dos primeras, se gestionen todos los cambios de animaciones.
 * 
 * ------------------------------------------------------------------------------------------------------------------------------------------------------
 * <<<<<<<<<<<<<<<<<<<<<<<<<<Nathan 03/07/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Animacion 3 Saltos
 * 
 * Para decirle al script cuando ha de usar el salto, he creado la booleana 'saltando' como la 'enMovimiento', así cuando se le da por primera vez
 * a la tecla de salto se activa, y permanece activa por si se quiere volver a saltar hasta que se reinicia al principio con false,
 * al igual que los currentJump
 * 
 * Lo que he hecho es que como hay una animacion diferente para cada salto, he creado las 3 animaciones y un estado en el animator para cada animacion
 * de manera que cuando el jugador pulsa la tecla de salto, salta la primera booleana de salto 1, que se cancela cuando vuelve a pulsar la tecla
 * activándose a su vez la del salto 2, después si vuelve a pulsarlo se cancela y se activa la del salto 3.
 * 
 * En el animator, todas están conectadas por la siguiente razón: Si he dejado de saltar en el salto2, necesito tener la transicion de corriendo,
 * al igual que si estoy en el salto 1, necesito la transicion al 2 sin pasar por idle o run.
 * 
 * He detectado un fallo y es que al hacer que anden hacia un lado u otro en el aire se puede solapar la animacion con uno de los saltos,
 * por eso propuse quitar esa funcion y dejar a los personajes cayendo ya que queda bastante bien (y se pueden seguir moviendo)
 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<<<<<<<<<<<<<Nathan 04/07/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Arreglo de Animaciones
 * 
 * Como comenté en el anterior diario, he arreglado el fallo de hacer la animacion de andar en medio del salto e interrumpir la del salto 3
 * 
 * Estuve intentando varias cosas del código y al final lo que funcionó fue en las condiciones del Animator, decirle al salto2 que
 * para pasar a la animacion de corriendo, la condicion de salto3 tiene que ser falsa también, así cuando se activa, no hay manera de que se solapen
 * 
 * 
 * ------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<<<<<<Xavi 05/07/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Arreglo Funcion de "DetectarMovimiento" en el script CharacterComun
 * 
 * He cambiado todo lo que estava echo por asignacion de teclas por axis Horizontal1 y Horizontal2 porque de la otra manera habia conflico entre las animaciones.
 * 
 * Tambien he ajustado los "has exit time" de las animaciones entre el salto y el idle porque habia un intervalo de tiempo y no era instantaneo, ahora si.
 * 
 * Ademas he simplificado las funciones de DirDetect y Movimiento en una sola funcion que asi ya no se activan las animaciones segun que tecla tocas sino tambien 
 * con el Axis y con esto tambien saber en que direccion miras
 * 
 * ------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Nathan 17/07/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto 1: Planteamiento de nuevo sistema de instancias
 * 
 * Al ir avanzando el proyecto me di cuenta de que con el sistema que Suri había implementado de instancias era muy básico para todo lo que necesitabamos
 * ir metiendole, como tener todos los datos completos de los personajes, si tienen armas... etc
 * 
 * Asunto 2: Creacion sistema de caracteristicas de los personajes y deteccion de players
 * 
 * Primero cree un script nuevo llamado ManagerCharacter que irá en un objeto empty junto al LandManager
 * Dentro cree la clase CharacterProperties con las propiedades basicas de cada personaje, obviamente se irán poniendo más conforme se necesite,
 * además puse la lista de CharacterProperties para verla desde el inspector
 * 
 * Para poder usarla por Scripts se necesita el public Characterproperties que hay dentro, indicando que se le pasen todos esos datos
 * 
 * Después cree en el Land Manager la class PlayerProperties, de esta manera como vamos a hacer los ataques y tal desde aquí, tendrá acceso
 * a todas las caracteristicas de los personajes seleccionados,
 * además de la lista de CharacterProperties, añadí un PositionInstance y un GameObject que será el prefab
 * 
 * Ahora como instanciarlos:
 * 
 * Nada más empezar le pongo en el inspector que los Players son 2 (esto se puede cambiar si en algun punto se usan más personajes a la vez)
 * y empiezo con el player1
 * 
 * Entro dentro de la lista de players seleccionando el primero y su lista de caracteristicas (ahora mismo vacía) la igualo
 * a las caracteristicas del jugaddor que se seleccionó en el menú (con las variables estaticas)
 * 
 * Para que me pase todas las caracteristicas, en el script del Managercharacter he creado la funcion GetCharacter
 * que requiere de un ID (la variable estatica de seleccion del player_1 o player2), una vez dentro le digo que me coja el Character que coincida con el ID
 * y me cree un nuevo CharacterProperties con todas sus caracteristicas que será el que le pase al LandManager con un return
 * 
 * Asunto 3: Programacion de instancias de los personajes
 * 
 * Una vez ya las caracteristicas del personaje en el script, selecciono el player que necesito entro en su Pf_Player puesto que así es como lo controlaremos
 * por el script y le digo que me instancie el Prefab que está dentro de sus caracteristicas, en la posicion Inicial (metida en el inspector) y con el Quaternion
 * 
 * Al cambiar la nomenglatura ahora de uso para los personajes, tuve que cambiar las demás lineas del Land para manejar los players con su Pf_Prefab,
 * además también fui script por script comprobando que los nombres estuvieran bien y cambiando lo necesario
 * 
 * Además solventé un error (que me tiró mucho rato identificar por qué era) y es que me daba fallo en que no entraba en el movimiento,
 * el problema estaba en que me daba error una cosa de sonido (porque no lo tengo yo) y omitía la linea siguiente
 * que es la que asignaba el Rigidbody del personaje, así que las cambie de orden y funcionó perfectamente
 * 
 * Asunto 4: Depuracion de Script LandManager
 * 
 * Como ya no se necesita borré la lista de tipo de personajes del LandManager porque solo tenia los prefabs de ambos, al igual que los gameobjects de player1 y player2,
 * además como tienen ya sus IDs dentro de las caracteristicas borré la lista de Ids y dejé como comentario la asignacion para recordar lo ya hecho
 * -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Nathan 22/07/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto 1: Poligonal Collider Plataforms
 * 
 * Coloqué los gráficos de las plataformas del Land2 y le puse los poligonal collider exactamente en la forma, retoqué el collider poligonal de la plataforma
 * grande que estaba un poco descolocado
 * 
 * Asunto 2: Prefab Cristian + Idle + Run
 * 
 * Le he colocado el arte a su prefab, así al instanciarlo ya no es un cubo verde
 * 
 * Además para que empiece a tener movimiento el menu (Alda está en idle cuando aparece) le he colocado a la imagen del prefab, el componente Animator, se lo añadí
 * al Anim de su script de CharacterComun para un futuro (me dio fallo y descubrí era esto) y creado la animacion de Idle
 * 
 * Teniendo tiempo le he creado también el Run, exactamente igual que a Alda para que se pueda usar en ambos (tened muy en cuenta quien haga los saltos
 * que han de llamarse las booleanas igual que las de Alda) y le he puesto la booleana de movimiento conectada Idle-Run para que funcione
 * 
 * Asunto 3: Arte en el Menu
 * 
 * He cuadrado las plataformas y los apoyos para que aparezca el personaje centrado en ellas, además he colocado el botón como el círculo de la plataforma
 * y añadido el fondo de la seleccion
 * 
 * He dejado provisionalmente ese tipo de cambio de botón aunque hay que ponerlo bien, no funciona adecuadamente ya que al hacer clic en otro botón se
 * quita el Tic verde del campeón 1 confirmado
 * 
 * Además le he colocado a los botones de los personajes su imagen correspondiente y al botón de re-elegir los personajes
 * ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<Xavi 22/07/2018>>>>>>>>>>>>>>>>>
 * 
 * Asunto 1 : Añadir sonidos.
 * 
 * He añadido el sonido en el land 1 y en el land2 tal y como explica Albert en los videos, creando un empty en la escena llamandole SoundManager donde he metido un script tambien llamado
 * SoundManager en este script lo que hay es una lista con los sonidos y estan identificados como veis en un index que asi puedo usar este indice en otro lugar para identificar cada sonido,
 * en este caso lo que he echo es crear una variable llamada SoundManager con el nombre SoundControl luego SoundControl lo he igualado a GameObject.Find("SoundManager").GetComponent<SoundManager>();
 * para que asi busque en la escena el empty SoundManager y dentro de este el script SoundManager de esta manaera tenemos acceso al soundmanager desde el script CharacrterComun,
 * y asi puedo llamar a los sonidos desde el script charactercomun con la linea " SoundControl.CreateSound(0); " el 0 es el indice del sonido que hay en ese numero dentro de la lista que he creado dentro del script SoundManager.
 * 
 * Asunto 2:
 * 
 * Añadir Background Land1 y mismo proceso del sonido, para añadir el background  he creado un canvas al cual le he metido la camara, dentro del canvas meto el background y ajusto este a la medida de la camera.
 * 
 * Asunto 3:
 * 
 * He echo que en todas las escenas el player 2 mire hacia la izquierda al empezar poniendo el Quaternion de la "Y" a 180grados.
 * 
 * -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Nathan 22/07/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * 
 * Asunto 1: Añadir animaciones de salto Cristian
 * 
 * He creado las animaciones de Jump1, Jump2 y Jump3 en el arte de Cristian y las he ajustado porque tienen que ir más rápidas que las de Alda, 
 * después me he metido en un su SkinAnim donde estaban el Idle y el Run, añadiendo las bools salto1, salto2 y salto3
 * y las he configurado entre las demás acciones para que funcione igual al de Alda
 * 
 * Asunto 2: Fondo Seleccion de escenarios
 * 
 * Como no tenía fondo ni nada puesto, el otro día se me ocurrió y propuse que el fondo al principio sea uno de ellos o un fondo en negro y cuando
 * se seleccione uno de los Lands, aparezca como fondo
 * 
 * Para hacerlo primero he creado el Image de fondo de dentro del canvas, como estaba a -9 todo, he tenido que decirle a los personajes que cuando se instancien
 * en el Menú (solo pasa en el menú) se sitúen a -9.5f que es donde se les ve por delante
 * 
 * Después he creado en el SelectLandManager un gameObject que será esa Image, para controlar qué Image es cree una Lista de Sprites
 * donde meteremos por el Inspector los Sprites que queramos
 * 
 * Después para hacerlo funcionar, me metí en las funciones de los botones que simplemente cogían el índice del Land para saber cuál cargar y les dije que
 * depende del Land que sea le cogieran el componente Sprite de la Imagen del gameObject background 
 * y lo igualaran al Sprite correspondiente de la lista de Sprites que cree antes
 * 
 * Además me di cuenta de que quedaba demasiado fuerte el color y yo lo que quería era una especie de fade out en grisaceo así que después, 
 * selecciono el componente Color del gameObject background que es la imagen de fondo y le digo que me lo iguale al color gris
 * 
 * El componente Color de una imagen es su opacidad, si la ponemos en negro es invisible así que el gris es el intermedio
 * 
 * He pensado que parar 'animar' si nos queda tiempo podríamos hacerle una animacion, en el que aparezca más pequeña y se ensanche (seria desde dentro de unity)
 * o podríamos hacer la imagen filled y hacer que inicie desde 0 y en 'x' tiempo se complete
 * 
 * Asunto 3: Correccion de tamaño de backgrounds Lands
 * 
 * Me di cuenta que al iniciar en cada Land se veían dos franjas azules (una arriba y otra abajo) porque el Image era más pequeño de lo que requería la pantalla, 
 * así que lo ajusté en ambos Lands para que apareciese correctamente 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 30/07/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto 1: Depurado del funcionamiento del Menú de selección de personajes.
 * 
 * El problema de que al pulsar en otro botón, después de confirmar un personaje, se quite el tic verde es por el tipo de animación que tiene el botón. Al seleccionar "Sprite Swap"
 * queda muy chulo el efecto de que ponga una imagen al pasar por encima, otra al pulsar y otra al soltarlo, pero tiene la pega de que el botón está "pulsado" hasta que pulsas en
 * otro sitio. Pasa a estar Disabled y, por tanto, cambia el sprite. La unica solución sencilla pero efectiva que se me ha ocurrido es que al pulsar, se habilite una imagen encima
 * con el icono del tic verde de confirmación. No se si os parece demasiado cutre, pero creo que funciona perfectamente y no perdemos el juego de que haga diferentes cosas al pasar
 * el ratón por encima. También antes te dejaba pulsar el botón de confirmar aunque no hubieras elegido ningun personaje, cosa que tb se soluciona de esta manera.
 * 
 * Luego, tal y como hemos comentado, he depurado un poco el script en sí y las interacciones entre los botones. Creo que el hecho de que las acciones de los botones estén tan
 * encadenadas unos con otros hace que se pierda aleatoriedad y sobre todo limita mucho cuando queramos meter más cosas, como más personajes o cualquier otra variante. Por ejemplo
 * ocurria que una vez que pulsabamos en Volver a elegir personajes, como el player 2 ya tenia confirmado un personaje y la condicion para que aparecieran los botones era precisamente
 * que el player 2 confirmara, se podía avanzar a la seleccion de escenario sin que el player 1 hubiera confirmado. Obviamente al estar pensado para jugar 2 en un mismo teclado
 * bastaria con hablar! pero creo que es un trabajo más fino a nivel de programación de este otro modo.
 * ------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 *  <<<<<<<<<<<<<<<<<Xavi 30/07/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 *  
 *  Puntos de deteccion de las paredes configurados correctamente, ademas en el apartado  donde se dice si es pared o suelo, en el apartado pared he añadido suelo para no tener que
 *  estar haciendo paredes y de esta manera hace las 2 funciones.
 *  
 *  ------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 *  <<<<<<<<<<<<<<<<<Nathan 31/07/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 *  
 *  Asunto: Ataque entre Players
 *  
 *  Primero en los Lands, al instanciar a los players se les asigna el tag de "Player1" y "Player2" (creados previamente) para que después las colisiones sean más sencillas
 *  
 *  Después le he creado el box collider de lo que sería su brazo dando un puñetazo, que se voltea también perfectamente (lo he hecho en Alda faltaría en Cristian)
 *  
 *  Ahora en el CharacterComun he añadido la funcion Collider y la bool puedeAtacar, la cual inicie como false y solo si colisiona con el tag
 *  "Player1" o "Player2" se ponga en "true", si no está colisionando con alguno de ellos deja de ser true
 *  
 *  Había conectado (en una de todas las pruebas) por medio de gameObjects el Land con el Character pero como no hacía falta quité el código, lo digo por si algún 
 *  día es necesario preguntadme y os explico en nada cómo lo hice
 *  
 *  En la funcion de Ataque le he dicho que si se pulsa la tecla F y la bool puedeAtacar del players[0] (es decir el player1) es true, haga la animacion y reste vida, si es false y se le da
 *  a la F (podemos poner otra tecla) haga solo la animacion y lo mismo con el el players[1] con el ShiftDerecho (podemos poner tambien el ctrl sino)
 *  
 *  He dejado creada la funcion RestarVida y señalado con comentarios donde y qué hay que hacer con ella (cómo llamarla lo he dejado explicado en un comentario en la funcion ataque)
 *  
 *  --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 31/07/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Restar de vida y gestión de los ataques
 * 
 * Para resolver el problema de que no conseguíamos que funcionara el sistema para restar vida, primero creé una variable int dentro de la class PlayerProperties en cada LandManager
 * con el nombre CurrentLife.
 * Luego creé una nueva función llamada "private int RestarVida(int ID)" en la que primero calculamos el valor del ataque, que es un número aleatorio entre la X y la Y que nos manda
 * el ManagerCharacter del player atacante. Después hacemos lo mismo con el valor de la defensa del receptor de ese ataque. Y por último hacemos la resta para tener el valor final
 * del ataque. Ese número es el que restaremos debajo del momento del ataque de la CurrentLife.
 * 
 * Después, dentro tb del sistema de ataques, se activaba la función puedeAtacar correctamente, pero no se desactivaba al alejarse del otro player. Se ha resuelto poniendo dicha
 * desactivación en otra función OnTriggerExit debajo de la de OnTriggerEnter.
 * 
 * Asunto: salto que contaba al dejarse caer el player de una plataforma
 * 
 * Al dejarse caer de una plataforma sin saltar, el sistema contabilizaba eso como un salto, y por tanto solo dejaba hacer 2 saltos a continuación en lugar de 3. El problema era que
 * la suma del Currentjumps estaba mal colocada dentro del CharacterComun. Poniendo esa suma dentro solamente de las pulsaciones de las teclas para saltar, se ha resuelto.
 * 
 * Asunto: Fallo de los sonidos al saltar
 * 
 * Parece que, por motivos que desconozco, el utilizar la variable "saltando" para activar o desactivar los sonidos, no respondía bien. A veces no saltaban los sonidos correctos o
 * bien algun sonido se repetía infinitamente hasta que se volvía a saltar o se tocaba el suelo de nuevo. Así que, dentro del CharacterComun, lo hemos resuelto usando la variable
 * "onAir" en su lugar y parece que responde mejor. Tb he tenido que crear una variable más llamada "firstJump" para indicar el primer salto, porque tb daba algo de guerra, aunque
 * aún no se exactamente por qué... Y aún así, a pesar de todo, alguna vez que saltas no genera el sonido correspondiente, aunque ocurre las menos veces. Cosas de Unity, supongo.
 * 
 * Asunto: Alineación del player "Sr. Mono" con el suelo
 * 
 * Me fijé que el player "Sr. Mono" no quedaba alineado con la superficie de las plataformas, con lo que los pies aparecían como metidos en el suelo. He ajustado los puntos 1 y 2
 * que delimitan el area de colisión del suelo en su prefab y ya queda alineado. Lo que no consigo averiguar por qué ocurre es que al saltar con 2 saltos (el menos es el caso que
 * he visto que pasa), cuando cae al suelo, la animación de saltar sigue activa unas décimas de segundo a pesar de que onAir ya está desactivado y el onFloor está activado. Así que
 * no se bien por qué ocurre, porque además solo pasa con Sr. Mono, no sucede con Alda...
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Nathan 01/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Diagnóstico de errores de Christian
 * 
 * Después de estar una hora revisando el script y el personaje, he detectado que hay un problema principal y grave en el salto de Christian: 
 * Hace 4 saltos porque uno no lo coge y se queda la animacion estática del anterior salto
 * 
 * También llegué a la conclusión de que el problema reside en el currentJumps, está hecho para que salto a salto vaya sumando 1 y al ser 3 no deje saltar más,
 * pero al empezar a saltar, empieza a sumar saltos al currentJump a partir del segundo salto por lo que hace 4
 * 
 * Asunto 2: Barras de vida
 * 
 * He añadido los GameObjects de Player 1 y Player 2 en el Land2Manager para meter ahí dentro toda la UI de cada uno
 * 
 * Después he añadido un UI Image, puesto el sprite de la barra de vida correspondiente, puesto como Filled y configurado correctamente,
 * tras eso la he duplicado y bajado el color a negro para hacerle el background.
 * 
 * En el Script he creado la variable LifeBar dentro del PlayerProperties para que cada player tenga la suya
 * y le he dicho que después de restar vida, iguale el FillAmount de la LifeBar del player correspondiente a su CurrentLife entre la vida inicial que tenía
 * para que la barra vaya disminuyendo
 * 
 * 
 *  --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 03/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: eliminar player cuando cae al vacío
 * 
 * Tras un buen rato de trasteo junto con Xavi mirando combinaciones, hemos conseguido eliminar al player de la lista de la cámara y re-instanciarlo de nuevo en el escenario. El
 * problema llega cuando intentamos eliminar el prefab que se cae al vacío. Tras luego revisar mucho rato, creo que el problema reside en cómo hemos creado todo el programa en sí.
 * No se muy bien explicarlo por escrito, pero podría resumirlo en que como hemos creado un sistema en que sí o sí tiene que haber 2 players en escena, en el momento en que eliminamos
 * uno, peta. No obstante, todo el que sepa o averigüe cómo resolverlo, es bienvenido.
 * Por el momento, lo he resuelto simplemente reposicionando al player en la posición original y restándole una vida. He dejado comentado en cada LandManager la opción de eliminar
 * el player y re-instanciarlo, por si encontramos la manera de resolverlo.
 * 
 * También he creado un texto (de momento de tamaño y forma provisional) que indica el numero de vidas que le quedan al jugador. Luego solo será cuestión de ponerlo bonito, pero el
 * sistema funciona (el de caer al vacío y que reste vida, ahora hay que hacer que las vidas vayan descendiendo si la barra llega a 0 y esta se rellene) 
 * 
 * Todo ello lo he implementado en ambos LandManager.
 * 
 *  --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Nathan 04/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto 1: Animaciones Attack
 * 
 * He subido al proyecto las animaciones de ataque de ambos Players. Posteriormente he creado los clips de las animaciones de Ataque, creando un sistema como el de los saltos
 * con booleanas para cada estado, que se activen y desactiven según el ataque deseado y luego vayan al idle o al run correspondiente. A cada clip le he desactivado el Loop Time
 * para que solo se haga una vez la animacion
 * 
 * He estado hora y media probando maneras de solucionar el siguiente error: Nada más terminar la animacion de un ataque necesita que salte el Idle o el Run, según el jugador
 * lo desee, y no lo hace puesto que en el script no se puede activar y desactivar una bool en el mismo frame para que funcione, ni tampoco podemos decirle que espere 'x' tiempo
 * para que la ponga en false
 * 
 * Por el grupo de WhatsApp de los programadores he comentado todas las opciones que he intentado, la última fue y tampoco lo conseguí:
 * 
 * Cambiar las booleanas por una sola int, cambiar el currentAttacks++ cada vez que se le de a la F por un currentAttack = true y un NumAttack++ cada vez que se le de a la F 
 * y fuera de la F que el currentAttack sea false, pero el problema es que no podemos funcionar por "en el siguiente frame" sea false porque una animacion dura 'x' frames y la
 * interrumpe que es el problema y por lo cuál necesitamos un "cuando termine la animacion empiece la siguiente" y eso por lo que he estado leyendo es con una maquina de estados 
 * de animacion
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 06/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto 1: Animaciones Attack
 * 
 * Después de repasar lo elaborado por Nathan, y probar varias posibles soluciones, se ha conseguido activar y desactivar las animaciones de ataque de Alda correctamente (las de
 * Christian aún hay que configurarlas, pero funcionan igual). El principal problema detectado es que la gestión del ataque en sí se realiza desde el LandXManager que corresponda,
 * pero su animacion al igual que las demás, están gestionadas desde el CharacterComun. Por ello hemos vinculado ambos scripts, para poder acceder a cualquiera de sus variables desde
 * el otro script.
 * 
 * En el Land2Manager (único Land donde se ha configurado de momento), cuando pulsamos el botón de ataque, activamos la bool de onAttack y ponemos a cero el contador para desactivar
 * las respectivas animaciones de ataque; ambas variables nombradas están ubicadas en el CharacterComun. También he puesto que el ataque que sea, salga de forma aleatoria bajo la
 * variable currentAttack, pero esto puede configurarse como nos plazca.
 * 
 * Después en el CharacterComun configuramos el funcionamiento de las animaciones de ataque dentro del Update, ya que si no el contador para desactivar las mismas no contaría en el
 * tiempo. Como la única manera que hemos encontrado de ajustar los tiempos para salir de las animaciones de ataque es personalizando en cada jugador el tiempo que dura su respectiva
 * animación, hemos condicionado cada animación en base al nombre del player que lo tenga (en el script se ve muy facilmente). Solo nos queda fijar el tiempo exacto que debe durar
 * cada una de las animaciones de cada personaje. Actualmente lo he dejado en 1 segundo, pero hay que ajustar todas.
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 08/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Unificar los script LandManager
 * 
 * Aunque inicialmente se acordó poner un LandManager por cada land por si queríamos personalizar alguna acción en un determinado land, al final vemos que estamos trabajando con esos
 * LandManager como si fueran los GameManager que conocemos del curso. Asi que hemos decidido unificarlos y asignar ese mismo script a cada Land.
 * 
 * Asunto: Detalles gráficos de los menús
 * 
 * Hemos reescalado los personajes en el menú de selección de personajes, para que aparezcan más grandes al elegirlos, ya que estéticamente queda mejor. También hemos ampliado la
 * escala de los personajes en el menú de selección de land.
 * 
 * Asunto: Sistema de vidas
 * 
 * Hemos completado el sistema de vidas del juego para que cuando la barra de vida llegue a cero, le reste una vida al player. Queda completar qué acción hacer cuando un player pierda
 * todas las vidas y darle un aspecto visual más chulo.
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 10/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Sistema de vidas
 * 
 * He retocado el sistema de vidas, porque me he dado cuenta que no estaba funcionando correctamente. Al llegar un player a cero vidas, este pasaba automáticamente a -1. Ahora
 * funciona como debe.
 * 
 * Asunto: Animaciones ataques
 * 
 * He incluido las animaciones de ataque de Christian. Ya están también ajustados los tiempos de cada animación para que pase a False en el momento en que termina la animación.
 * Estos tiempos están ajustados personalizados para cada tipo de player.
 * También he resuelto un fallo que no podías girar al personaje si estaba en medio de un ataque.
 * 
 * Asunto: Velocidad y gravedad del juego
 * 
 * He acelerado un poco la velocidad de los personajes y sus respectivas animaciones, ya que se movían demasiado despacio, el juego perdía mucho dinamismo. No me ha dado tiempo a
 * ajustar la gravedad, pero es muy sencillo y rápido.
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 13/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Animaciones en menú de selección de personajes
 * 
 * He incorporado las animaciones de los iconos de los personajes cuando se seleccionan. He probado todas las combinaciones de selección y funcionan correctamente.
 * 
 * Asunto: Visualizar valor del daño en cada ataque
 * 
 * He incorporado en el LandManager que se visualice sobre las barras de vida durante un breve espacio de tiempo el valor del daño recibido. Hace una pequeña animación la cifra y
 * desaparece.
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Nathan 14/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Ajuste de gravedad
 * 
 * Estuve toqueteando la gravedad de cada player y decidí dejarlo en 1.15 porque en 1 parecía que flotaban y en el 2 era demasiado, se quedaban cortos los saltos y parecía
 * muy muy forzado
 * 
 * Asunto: Ajuste de velocidad
 * 
 * Christian tenía una velocidad muy alta y su personaje parecía más un avión que otra cosa, así que la he reducido pero la he puesto un poco más alta que Alda, así que
 * habrá que ponerle menos ataque para balancear eso
 * 
 * Asunto: Añadidas animaciones Hit + Die Christian y Alda
 * 
 * He añadido éstas animaciones y además he dejado ya creada la Hit.anim, con su correspondiente booleana Damage y las transiciones a la espera de decidir cómo vamos a hacer
 * exactamente éste sistema ya que al no ponerle cuándo ataca cada uno, ambos pueden atacarse a la vez las veces que quieran y se solaparían las animaciones de Hit
 * 
 * Asunto: Balanceo de ataque y defensa de los personajes
 * 
 * Para hacer el juego más interesante, he hecho un balanceo de Skills, Christian es 3 puntos más rápido y tiene más defensa pero Alda es más fuerte que Christian
 * y tras varias pruebas quietos uno contra otro quedan siempre casi a la par
 * 
 * Asunto: Error detectado
 * 
 * El personaje de Christian se queda pillado en las 'paredes' de las 2 plataformas del Land 2
 * 
 * Asunto IMPORTANTE: Implementación sistema de Dañado/Hits
 * 
 * Como ambos players podían pegarse indefinidamente y se solaparían las animacions de Hit, he empezado creando dos booleanas en el LandManager para manejar quién recibe
 * el daño y quien ha atacado
 * 
 * Si la booleana es falsa, significa que se puede atacar por lo tanto tras restarle la vida al player correspondiente, le digo que la booleana de daño de ese personaje sea true
 * para que no pueda atacar y que en el CharacterComun donde se controla su animacion, la booleana correspondiente se active también
 * 
 * Ahora vamos al Character Comun, también hay 2 booleanas de daño para cada personaje inicializadas en false, y simplemente cuando son true, activan la animacion correspondiente
 * del player correspondiente y tras x segundos desactiva la animacion y vuelve a false tanto la booleana de daño del CharacterComun como la del LandManager, permitiéndole atacar de
 * nuevo.
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 16/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Inclusión de máquina de estados
 * 
 * He incluído una máquina de estados en el LandManager, ya que así es más sencillo incorporar una cuenta atrás antes de empezar los combates y un estado final al morir uno de los
 * personajes. Lo único que he hecho es hacer un estado INIT, que contiene la cuenta atrás como tal, un estado BATTLE, que contiene todo lo realizado hasta ahora, y un estado FINAL,
 * que activará las animaciones de muerte que correspondan además de mostrar un texto del jugador que vence.
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Nathan 16/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Muerte de los personajes
 * 
 * He modificado el estado de FINAL porque no hacía falta diferenciar más allá de qué player es el que muere. Simplemente he dicho que el que no gane le mande
 * al CharacterComun la booleana dead como true y haga la animación de muerte.
 * 
 * También cambié para que el texto de quién gana se haga después de la animación porque no se apreciaba bien.
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 17/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Ataque especial
 * 
 * Hemos incluído en el LandManager la función de que cada personaje tenga un ataque fuerte que podrá usar en determinadas ocasiones. Hay en el canvas una barra debajo de la de vida.
 * Esta barra se va cargando a medida que un jugador consigue acertar ataques en el rival. Cuando la barra llega al 100%, los players pueden pulsar "R" (player 1) o "-" (player 2)
 * para lanzar un ataque más potente. Por el momento dejo esa parte del script comentada para que no funcione hasta que tengamos las animaciones correspondientes.
 * 
 * Para el ataque especial también he creado debajo una nueva funcion llamada RestaVidaSpecial, que funciona igual que la de RestarVida normal, pero que hace que la fuerza de ataque
 * se multiplique por 3, para que el golpe sea más potente pero siga teniendo en cuenta los rangos defensivos del otro jugador.
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Nathan 18/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Retroceso al recibir daño
 * 
 * Lo que he hecho ha sido crear 2 booleanas para cada player, que se activan respectivamente al hacerles daño al susodicho player, tuve que poner lo que hace cuando 
 * retrocede fuera porque sino, unicamente funcionaba cuando se le daba al boton 'f' o 'shift'.
 * 
 * Una vez dentro de la booleana, he creado el Vector3 targeted (la posicion hacia la que va a ir) y le he indicado
 * que sea la posicion del player x + un random para que no retroceda siempre lo mismo (tuve un fallo y es que como estamos en un script que no es de los players en si
 * no puse que fuera la posicion del player y me tomaba el punto 0, 0, 0 que es donde está 'colocado' el LandManager para si le pasa a alguien alguna vez ande con ojo),
 * después he creado la linea del movimiento en sí con un Lerp, diciendole que el player sea la posicion inicial, targeted hacia donde va y cree también una SpeedTarget
 * que es la velocidad a la que retrocede.
 * 
 * Para desactivarla, al principio me costó pensar cómo hacerlo porque se quedaba en bucle y no salía del true pero repitiendolo lo vi claro,
 * el retroceso dura lo que dura la animación de daño y en el otro script en cuanto termina la animacion de daño la devuelve como false,
 * así que cuando esa bool de la animacion es false, que la desactive.
 * 
 * Una vez eso me funcionaba, le dije que si mirandoIzq era true le sumara a la x y si mirandoIzq es false le restara a la x para hacer el retroceso correctamente y
 * se lo hice a los dos players.
 * 
 * Lo he hecho de ésta forma pensando en el ataque especial, que queríamos que salga despedido y creo que con ésto hecho funcionará perfecto
 * 
 * 
 * Asunto: En el Land 1 Bug en el Canvas con el Scale with Screen (Solucionado)
 * 
 * En algún momento sin querer se cambió la escala a Constant pixel size y ahora cuando la he ido a poner en Scale with Screen no funciona correctamente y sale todo
 * absolutamente movido
 * 
 * Solucion: Por si os pasa alguna vez he dejado éste error aquí como solucionado, le he puesto el scale with screen y hay que poner en el Reference Resolution la Resolucion
 * desde la que parte (en este caso 1920 x 1080) y así funciona ya correctamente al cambiar la resolucion
 * 
 * Hay que hacer lo mismo en el Land2 que está puesta la resolucion mal: 800 x 600 y reescalarlo todo porque al cambiarla a la correcta se desestructura todo
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 20/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Movimiento de retroceso con AddForce
 * 
 * Al usar Lerp para el retroceso, el movimiento que hace el player al recibir un ataque es muy lineal, no tiene un impulso inicial y una posterior desaceleración. Creemos que la
 * clave será utilizar mejor el AddForce. El problema es que estamos haciendo pruebas y no responde como debería.
 * 
 * He creado un Quad de prueba con un script llamado PRUEBA. En dicho script he creado una función sencilla para que, al pulsar el return, el quad salga disparado hacia arriba y la
 * derecha, como si recibiera el ataque especial que más adelante se implementará. En principio funciona perfectamente.
 * 
 * Pero al intentar implementarlo en los players del juego, no funciona. Dentro del LandManager, he creado una variable float ForceAttackSimple para dar una fuerza al ataque sencillo
 * y otras 2 float para dar una nueva posicion X e Y, donde desplazaremos al player que recibe el golpe. Después, en el momento de recibir el golpe y descontarlo de su barra de vida,
 * calculo la nueva posición X (NewPosX) y activo la booleana retroceso2 (para probar solo con el player 2). Luego dentro del Update, le digo que aplique el AddForce dándole el nuevo
 * Vector2 de destino multiplicado por el ForceAttackSimple. Debería funcionar pero la realidad es que no funciona: la NewPosX se calcula cada vez que recibe un golpe, pero el player2
 * no se desplaza excepto si está cerca de la posición x = 0 (no tengo idea de por qué...).
 * 
 * Asunto: Ajuste de gravedad y fuerza de salto
 * 
 * He aumentado la gravedad de los players asi como las fuerzas de salto, ya que así le damos un ritmo más arcade a los personajes. Antes saltaban demasiado despacio y parecía que
 * flotaban. Ahora, con el ajuste que se hizo de la velocidad de desplazamiento, el efecto es bastante más dinámico.
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Nathan 21/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Añadido Arte HUD
 * 
 * He añadido y colocado en ambos Lands el arte de la HUD (barra de vida y ataque especial)
 * 
 * Asunto: Descarte de Addforce
 * 
 * He estado trasteando toda la mañana con el Addforce pero nos ha estado dando fallos con el mismo script en diferentes Lands, conseguí que funcionase algo mejor pero 
 * muy lejos de lo necesitamos, así que por falta de tiempo vamos a dejar ésta mejora para el juego totalmente hecho
 * 
 * Asunto: Resolucion error atacar por detrás
 * 
 * Nos dimos cuenta de que había un error que no habíamos probado y es que cuando un player atacaba al otro por la espalda no hacía el ataque ni sucedía nada.
 * 
 * El error estaba en que estaban mal puestas las colisiones, como tenemos dos solamente se detectaban entre ellos las de los puños, pero tenía que detectar el cuerpo entero
 * y era problema de jerarquía, lo cuál solucioné desde el prefab, después me di cuenta de que seguía dando error cuando los personajes se daban la vuelta
 * una vez ya habían 'colisionado' pero todavía no habían salido de la colisión, así que añadí un OnTriggerStay2D para mientras esté colisionando el puedeatacar sea true
 * ¡y resuelto!
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 22/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Resolución de menús y tipo de letra
 * 
 * He aplicado la fuente "Venus Rising", que ha sido la elegida para la UI del juego, en los lands. He ajustado también el tamaño de las mismas.
 * 
 * También he cambiado la resolución de los canvas del menú de selección de personajes y selección de lands, que estaba en 800x600, ahora está en 1920x1080.
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 23/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Corrección de ataques por la espalda
 * 
 * Detectamos que cuando un jugador ataca por la espalda a otro, el retroceso lo hacía hacia la espalda del que recibe el ataque. Esto se debía a que estabamos condicionando la
 * dirección del desplazamiento del retroceso en función de si el jugador que recibe el golpe miraba a la izquierda o a la derecha. He corregido este problema condicionando la
 * dirección a la posición del jugador que golpea respecto al que recibe el golpe. De esta forma, es indiferente hacia donde mira, pero si le golpean desde la izquierda se desplazará
 * a la derecha, y al revés.
 * 
 * Asunto: Animaciones de ataques ultimate
 * 
 * Tras recibir las animaciones de los ataques ultimate, las he integrado en el proyecto. He reasignado estos ataques ultimate a las teclas C y RightCtrl para los jugadores 1 y 2
 * respectivamente. Las teclas R y Minus quedaban algo incómodas para la mano.
 * 
 * Como siempre, el sistema de acción del ataque, que esta vez está condicionado a que cuando pulse la tecla del ataque ultimate tiene que tener cargada la barra de energia para que
 * se active, lo he ubicado en el LandManager, junto a los ataques sencillos. Las animaciones como tal estan ubicadas en el CharacterComun.
 * 
 * El principal problema ha surgido porque en estas animaciones el momento en que el personaje golpea al rival no se produce al inicio de la animación, sino varios fotogramas después.
 * He intentado que el retroceso del jugador que recibe el golpe se produzca unas décimas de segundo más tarde para que cuadren las animaciones, pero por algun motivo que no he
 * conseguido resolver (tras más de 3 horas de intentos) no hace el retroceso. Para "disimular" un poco esto, he ajustado las animaciones omitiendo algun fotograma para que el golpeo
 * esté lo más cerca posible del inicio de la animación. Aunque no queda perfecto, al menos queda sensiblemente mejor.
 * 
 * El ataque ultimate de Alda tiene tambien unos rayos que saldrían cuando toca el suelo con la mano. Quedaría por crear una animación más para los rayos, aunque creo que no puede ser
 * asignada al Skin de Alda, ya que tiene que ser una animación simultanea a la del ultimate. Creo que la solución puede ser crear un GameObject Empty que se instancie en un momento
 * determinado y cargue la animación de los rayos.
 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 *
 * <<<<<<<<<<<<<<<<<Nathan 23/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 *
 * Asunto: Animacion Ataque Ultimate Alda
 * 
 * Había que hacer que el rayo saliera a la par que la animación, para lo cuál cree un gameobject empty con un script unicamente para él y lo convertí en prefab
 * 
 * Después le coloqué dos animaciones, una vacia de 'idle' y otra con la animacion de los rayos, en el LandManager se instancia si el player es el personaje de Alda,
 * y tras eso saldrá hacia un lado o hacia el otro depende de dónde esté el personaje enemigo como acordamos anteriormente.
 * 
 * Una vez se sepa hacia donde va, se reposiciona el UltimateTargeted que es donde se va a instanciar y en la linea de Instancia se gira en el Quaternion si es necesario
 * 
 * Luego conecto el script del Rayo con el del LandManager y le digo que la booleana de ese script llamanda Rayos sea true, cuando sea true pasados 0.3f que es lo que tarda
 * Alda en poner la mano en el suelo en la animacion, salen los rayos y tras otro instante se vuelve false y se elimina el mismo gameobject
 * 
 * Hecho en ambos lands
 *
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Nathan 29/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Introduccion del juego implementada + Todo el arte metido en el proyecto
 * 
 * He creado la escena de Intro del juego, con los botones funcionando y todo su arte correspondiente
 *
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 29/08/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Reordenar escenas en Build Settings
 * 
 * He reordenado las escenas en el Build Settings ya que al incluir ahora el MainMenu y el video de intro se han desordenado. De esta manera ahora estan en orden (1- video intro,
 * 2- Main menu, etc...). También he corregido tanto los cambios de escenas en sus scripts correspondientes para que funcionen correctamente, como los índices de los lands en el menu
 * de seleccion de lands, para que el ID asignado sea el de la escena correcta.
 * 
 * Asunto: Video introducción
 * 
 * Dado que tenemos un video de intro, he renombrado la escena "Intro" y el script "IntroManager" a "MainMenu" y "MainMenuManager" respectivamente. Solamente para evitar confusión con
 * la escena y script que realmente llevan la intro.
 * 
 * He creado una escena inicial (escena 0) que contenga y cargue el video animado que ha creado Sofía al arrancar el juego. Con Premiere, he tenido que extraer aparte el audio del
 * video, ya que a Unity, hasta donde he podido ver, pide meterlos por separado. Hecho esto, en la MainCamera he añadido el componente VideoPlayer, donde le he asignado el video y
 * audio respectivamente. Sólo con esto, ya se reproduce el video cuando se inicia la escena. Luego he creado un script llamado VideoIntroManager, donde primero he creado una booleana
 * llamada isPlaying para poder tomar acciones que queramos mientras se reproduce, y luego simplemente con un contador detengo el video cuando llega al final cambiando de escena.
 * También hay otro contador con el que hacemos aparecer un texto en parpadeo que nos permite saltar la intro si queremos.
 * 
 * Asunto: Agregar y retocar parte del arte
 * 
 * He añadido los botones creados por Isma en las escenas de seleccion de campeones y seleccion de lands, y tambien los marcos que diseñó Alda para resaltar las imágenes de las lands
 * posibles. También en la escena de selección de lands he creado un botón para poder volver a la selección de campeones si queremos cambiar el personaje con el que vamos a jugar.
 * 
 * Asunto: Botones al terminar un combate
 * 
 * He creado unos botones para volver al MainMenu o al de seleccion de personajes cuando un jugador vence al otro. Estos botones aparecerán unos segundos despues de aparecer el texto
 * "Player X wins!". Sin embargo, tras mucho revisar, no entiendo por qué esos botones no responden. Al pulsarlos no ocurre nada, a pesar de que en teoría están correctamente
 * configurados. Por el momento los dejo así y mañana lo revisamos.
 *
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 03/09/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Asunto: Tiempo final del video intro
 * 
 * He recortado un segundo y medio la duración del video de la intro, para que cambie antes al Main Menu, ya que quedaba demasiada pausa en negro.
 * 
 * Asunto: depurado de algunas variables
 * 
 * He quitado la variable onAir, ya que no tenía sentido que existiera si ya teniamos un onFloor. Realmente, si onFloor es true significa que onAir es false, y viceversa. Por tanto
 * con tomar como referencia el onFloor nos vale.
 * 
 * Asunto: Ajuste en sistema de vidas
 * 
 * He detectado que cuando un jugador llegaba a cero vidas por caerse de la plataforma, en vez de por perder su barra de vida, el player no moría. Ya está resuelto. También he
 * arreglado que cuando el personaje se caía de la plataforma con algo de vida quitada (por ejemplo al 70%), se le restaba una vida pero la barra de vida no se rellenaba, sino que
 * seguía igual.
 *
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * <<<<<<<<<<<<<<<<<Alberto 15/09/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 * 
 * Corregido el problema de que los botones que aparecen al terminar una batalla no funcionen. Hemos re-incluido el EventSystem en el Land1.
 * 
 * También he creado las escenas para el RulesMenu y el CreditsMenu, a falta de poner los textos correspondientes en su interior.
 * 
 */