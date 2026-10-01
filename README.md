# Quiz Knight IA

Este repositorio contiene un prototipo jugable de Unity 2D para el proyecto "Quiz Knight IA: Metroidvania Educativo con Inteligencia Artificial Aplicada".

## Estado actual

La versión actual ya incluye:

- Sistema de jugador con vida, ataque básico y mejoras
- Jefes con estados (Idle, Chase, Attack, Question, Enraged, Defeated)
- Preguntas académicas cargadas desde JSON en `Assets/Resources/Questions`
- Generación aleatoria de rutas por zonas
- IA basada en DFS, BFS, A* y árboles de decisión
- Pantalla de Game Over con estadísticas
- Arranque automático del prototipo en Play Mode mediante `GameBootstrap`

## Importante

Es un prototipo jugable funcional de la lógica del juego, no un juego completo con arte final ni niveles visuales avanzados. Sirve como base sólida para continuar desarrollo en Unity.

## Requisitos

- Unity 2022.3 LTS o superior
- Proyecto 2D

## Cómo probarlo

1. Abre este repositorio con Unity Hub.
2. Crea/importa el proyecto en Unity 2022.3 LTS.
3. Presiona Play.
4. El arranque automático crea los objetos del prototipo y el juego comienza.

## Estructura principal

- `Assets/Scripts/AI` : algoritmos de IA
- `Assets/Scripts/Gameplay` : lógica del prototipo
- `Assets/Scripts/Managers` : gestión general del estado del juego
- `Assets/Scripts/Runtime` : bootstrap del juego
- `Assets/Resources/Questions` : JSON con preguntas por materia

## Objetivos académicos implementados

- DFS
- BFS
- A*
- Máquinas de Estados Finitos
- Árboles de Decisión
- Aleatorización controlada
