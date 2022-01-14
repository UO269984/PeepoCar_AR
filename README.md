# PeepoCar_AR
Práctica de realidad aumentada para la asignatura RAA

* [Demo de la APP](https://youtu.be/pC7-XuhRPt8)
* [Enlace de descarga de la APK](https://github.com/UO269984/PeepoCar_AR/releases)

## Para ejecutar el proyecto en Unity
Para iniciar el proyecto en Unity hay que añadir estos dos archivos al proyecto, se pueden encontrar en [este enlace](https://unioviedo-my.sharepoint.com/:f:/g/personal/uo269984_uniovi_es/EmwKYZa2sqdEqC8cghZ8Y7YBNmqAV4Fjy6T2rtAeL_riGw?e=2pZscA).
* Packages/com.ptc.vuforia.engine-10.3.2.tgz
* Assets/Resources/VuforiaConfiguration.asset

## Que tiene la aplicación?
En la primera escena nos aparece el PeepoCar, coche del juego de de VR [PeepoVRoad](https://github.com/PeepoVR/PeepoVRoad). En la segunda escena podremos ver un alien en las coordenadas: (43.536518, -5.705373).

En ambas escenas se usa [este marcador](https://raw.githubusercontent.com/UO269984/PeepoCar_AR/main/Marcador.jpg) para posicionar los objetos 3D, puedes imprimirlo, o poner en otro teléfono la imagen y apuntarla con la aplicación.

### Escena del PeepoCar
En esta escena se puede interactuar con el PeepoCar clicando en la pantalla del teléfono. Se puede interactuar con:
* Peepo Logo frontal
* Luces del coche (encender y apagar)
* Capó (encender y apagar el motor)
* Retrovisores (abrir y cerrar)
* Volante (tocar el claxon)
* Texto de dar una vuelta en el PeepoCar (montar en el PeepoCar e ir a buscar el alien, siguiente escena)

Además se ha añadido un panel encima del coche que muestra sus características, al estilo videojuego.

### Escena del Alien
En esta escena se puede ver un alien en las coordenadas (43.536518, -5.705373). Cuando nos acercquemos a menos de 80 m del punto del alien, la aplicación nos avisará de que estamos en una zona en la que se han visto aliens. Si nos acercamos a menos de 25 m nos saldrá el alien. Si apuntamos con la cámara al marcador el alien saldrá en 3D, en caso contrario solo saldrá una imagen.