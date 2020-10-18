PEC 1 - Un juego de carreras
============================

¿Qué es?
--------

Se trata de un juego de carreras de coches en modo competición contrarreloj.

En el menú de inicio el usuario puede eligir en qué circuito competir y con qué
coche hacerlo. También, des de el mismo menú, podrá iniciar una carrera —de una
duración prefijada de tres vueltas completas al circuito— donde deberá competir
contra el mejor tiempo que el mismo jugador haya hecho en ese circuito.

Una vez terminada la carrera se muestra su repetición desde distintos ángulos,
así como también des de la cámara del coche para que el jugador pueda aprender
de sus propios errores. También se muestra durante la carrera una reproducción
del mejor tiempo en forma de coche fantasma.

En cualquier momento de la carrera o de la repetición el jugador puede pulsar la
tecla «Escape» para pausar el juego. Se mostrará entonces un menú desde el que
el jugador puede elegir empezar de nuevo la carrera o volver al menú principal.

¿Qué se ha implementado?
------------------------

* La lógica del juego consistente en carreras de tres vueltas con registro del
  tiempo total de la carrera y de los tiempos individuales de las vueltas.
* Un circuito de montaña con muchos detalles y otro mucho mas simple que ha
  servidor para hacer pruebas durante el desarrollo.
* Definición de dos coches de colores y configuraciones diversas. El modelo
  3D es el mismo para ambos coches.
* Detección del terreno dónde se encuentran las ruedas del coche y modificación
  de su fricción según el material del terreno. De esta manera, el coche irá
  más rápido o más despacio según las condiciones del terreno.
* Reproducción de la carrera con mejor tiempo en forma de coche fantasma.
* Repetición de la carrera des de la cámara del coche y también des de distintas
  cámaras distribuidas por todo el circuito.
* Menú para poder seleccionar el circuito así como también el coche con el cual
  se va a competir.
* Almacenaje en formato JSON de la mejor carrera en las preferencias de usuario
  para que pueda recuperarse en la siguiente sesión de juego.
* Se ha añadido música al menú de inicio y en la escena de repetición de carreras.

Detalles de la implementación
-----------------------------

El código fuente tiene una organización diferenciada en tres partes:

* En el directorio «Scripts/Models» pueden encontrarse los modelos de datos.
  Son los objetos dónde se almacenan las grabaciones de la carreras, así cómo
  las configuraciones de circuitos y coches de carreras.
* En «Scripts/Scenes» están los controladores de las escenas que componen la
  lógica del juego. Estos controladores se diferencian así mismo entre
  controladores de lógica y de presentación de datos.
* La última carpeta, «Scripts/Shared», contiene la definición de servicios,
  eventos y controladores compartidos entre las distintas escenas.

Así, en la carpeta «Shared», pueden encontrarse los servicios «GameRecorder» y
«GameReplayer» que són los que se encargan de la grabación y reproducción de las
carreras. Para hacerlo deben configurarse con la ranura de almacenaje que se
usará —el modelo «Recording», que son dos ScriptableObject distintos para cada
circuito— y los objetivos que hay que grabar/reproducir —estos se especifican
en los «Prefabs» de los coches/fantasmas; en este caso las ruedas delanteras,
la carrocería, así como también las luces frontales y traseras.

Para la grabación se toman muestras de la posición, rotación y activación de
cada uno de los objetivos. Durante la reproducción estas muestras se interpolan
en cada _frame_ a los objetivos definidos en los objetos destino.

El servicio «SetupLoader» del directorio «Shared» sirve para instanciar los
_Prefabs_ de circuitos, coches y fantasmas en las escenas de carrera y repetición
según lo que esté especificado en «PlayerPrefs». Luego las escenas los identifican
según la etiqueta de cada objeto («Circuit», «Ghost» o «Player»). De esta
identificación se encarga la clase «Behaviours/HasRaceComponents».

Respecto a los controladores compartidos, los más relevantes son «CircuitController»,
que encapsula la lógica de un circuito, y «WheelStiffnessController», que se encarga
de modificar la fricción recibida en las ruedas respecto al material físico sobre el
que se están desplazando.

Todos los circuitos deben contener un controlador «CircuitController» que ha de
configurarse con la referencia a una transformación «polePosition», que especifica
dónde deben situarse los coches al inicio de la carrera, y un conjunto de
colisionadores «controls» que sirven para definir qué se considera una vuelta
completa al circuito.

En cada coche está definido un colisionador de tipo «trigger» etiquetado como «ControlTarget». Cuando este objeto ha colisionado con todos los controles del
circuito en el orden que se han definido se invoca el evento «CircuitCompletionEvent»
del mismo. Este evento es recibido por el controlador de la carrera («RaceController»)
que ejecuta la lógica necesaria al dar una vuelta por concluida.

«WheelStiffnessController» por su parte detecta de una manera muy simple sobre
qué tipo de terreno se desplaza el coche. Cada controlador de este tipo tiene
una referencia al «WheelCollider» de una rueda y a esta a su vez se le ha
añadido un colisionador de tipo «trigger» que es el que nos indica cuando la
rueda ha cambiado de terreno. Para saber la fricción del terreno se consulta
el material asignado al mismo («PhysicMaterial»).

Esta solución resulta muy flexible y efectiva si el terreno está bien definido.
Otra posibilidad habría sido usar el método «GetGroundHit» de «WheelCollider».

Vídeo de demostración
---------------------

![Demo](Demos/demo.webm)

La última versión
-----------------

Puede encontrar información sobre la última versión de este software y su
desarrollo actual en https://gitlab.com/joansala/uoc-carreras

Referencias
-----------

* Lukas Bobor (2019). _Medieval Buildings Exteriors_ [Modelo 3D].
Recuperado de https://assetstore.unity.com/packages/3d/environments/historic/medieval-buildings-exteriors-72836
* 4Knights Entertainment (2017). _Sword Attack Music Pack_ [Música]. Recuperado de https://assetstore.unity.com/packages/audio/music/sword-attack-music-pack-52081
