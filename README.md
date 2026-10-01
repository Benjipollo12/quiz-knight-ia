# Quiz Knight IA

Proyecto base para Unity 2D del juego educativo "Quiz Knight IA".

## Requisitos

- Unity 2022.3 LTS o superior
- 2D Template
- TextMeshPro (se instala desde Package Manager si hace falta)

## Estructura principal

- Assets/Scripts: lógica del juego
- Assets/Resources/Questions: preguntas en JSON
- Assets/Scenes: escena principal

## Cómo abrirlo

1. Clona este repositorio
2. Abre la carpeta con Unity Hub
3. Selecciona la versión Unity 2022.3 LTS+
4. Abre la escena `Assets/Scenes/Game.unity`

## Controles

- A / D: mover
- Espacio: saltar
- J: atacar
- Cuando el jefe entra en rango, aparece una pregunta
- Correcta: +2 daño y +5 HP
- Incorrecta: -1 intento y -5 HP

## Objetivo

Superar al jefe, responder preguntas académicas y mantener la vida del personaje.
