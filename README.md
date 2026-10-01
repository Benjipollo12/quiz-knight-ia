# Quiz Knight IA

Juego 2D tipo Metroidvania educativo con IA aplicada. Proyecto base listo para abrir en Unity.

## Descripción

Quiz Knight IA es un videojuego 2D de plataformas y combate inspirado en Mega Man y Hollow Knight. El jugador explora zonas temáticas de:

- Programación
- Circuitos
- Redes
- Bases de Datos
- Electrónica
- Inteligencia Artificial

Cada partida genera aleatoriamente el orden de las zonas y el jugador debe avanzar derrotando jefes, resolviendo preguntas y mejorando su personaje con NPCs permanentes.

## Contenido del proyecto

- Sistema de vida y combate del jugador
- IA de jefes con A* y máquinas de estados finitos
- Preguntas académicas cargadas desde JSON
- Generación de rutas con DFS y BFS
- Árboles de decisión para decisiones del enemigo
- Pantalla de Game Over con estadísticas finales
- Estructura de Unity preparada para ser extendida

## Requisitos

- Unity 2022.3 LTS o superior
- Configuración 2D

## Cómo abrir en Unity

1. Descarga o clona este repositorio.
2. Abre la carpeta con Unity Hub.
3. Selecciona Unity 2022.3 LTS o superior.
4. Crea la escena principal y añade a la jerarquía los prefabs de jugador, jefe y UI.
5. Usa `GameManager`, `QuestionManager`, `LevelGenerator`, `PlayerController` y `BossController` como base.

## Estructura principal

- `Assets/Scripts/AI` : algoritmos de IA
- `Assets/Scripts/Gameplay` : lógica del juego
- `Assets/Scripts/Managers` : controladores globales
- `Assets/Resources/Questions` : preguntas en JSON

## Objetivos académicos incluidos

- DFS
- BFS
- A*
- Máquinas de Estados Finitos
- Árboles de Decisión
- Aleatorización controlada

## Notas

Este repositorio contiene una base funcional y educativa para que puedas continuar el proyecto en Unity, adaptándolo a tu estilo visual, música, arte y nivel de juego final.
