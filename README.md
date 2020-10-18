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
