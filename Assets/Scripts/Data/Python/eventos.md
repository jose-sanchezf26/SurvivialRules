# Eventos de Survival Rules

| User  | Game ID  | Session ID | Event Type | Time  | Data  | Descripción  |
|-------|-------------|------------------------------------|------------|---------------------|-------|---------------|
| Jose  |           | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-log_in  | 17/02/2025 17:01:21 | {}    | Inicia sesión |
| Jose  |           | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-log_out | 17/02/2025 17:18:20 | {}    | Cierra sesión |
| Jose  | Jose - 17/02/2025 17:18:07 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-start_game | 17/02/2025 17:18:20 | {} | Empieza una partida |
| Jose  | Jose - 17/02/2025 17:18:07 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-end_game | 17/02/2025 17:20:20 | {} | Termina una partida |
| Jose  | Jose - 17/02/2025 17:24:12 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-open_fb | 17/02/2025 17:24:21 | {} | Abre la base de hechos |
| Jose  | Jose - 17/02/2025 17:01:22 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-close_fb | 17/02/2025 17:01:43 | {} | Cierra la base de hechos |
| Jose  | Jose - 17/02/2025 17:24:12 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-open_kb | 17/02/2025 17:24:20 | {} | Abre la base de conocimientos |
| Jose  | Jose - 17/02/2025 17:24:12 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-open_rules_log | 17/02/2025 17:24:19 | {} | Abre el log de las reglas |
| Jose  | Jose - 17/02/2025 17:24:12 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-open_inventory | 17/02/2025 17:24:16 | {} | Abre el inventario |
| Jose  | Jose - 17/02/2025 17:24:12 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-start_tutorial | 17/02/2025 17:28:09 | {} | Empieza el tutorial |
| Jose  | Jose - 17/02/2025 17:24:12 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-end_tutorial | 17/02/2025 17:28:22 | {} | Termina el tutorial |
| Jose  | Jose - 17/02/2025 17:24:12 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-create_block | 17/02/2025 17:29:18 | {"blockType": "WhenPlayClicked", "blockId": "84"} | Crea un bloque |
| Jose  | Jose - 17/02/2025 17:24:12 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-delete_block | 17/02/2025 17:37:01 | {"blockType": "WhenPlayClicked", "blockId": "84"} | Elimina un bloque |
| Jose  | Jose - 17/02/2025 17:24:12 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-drop_env | 17/02/2025 17:29:19 | {"blockType": "WhenPlayClicked", "blockId": "84", "positionX": 209.94165, "positionY": -275.427, "sbr": "<Block>...</Block>"} | Deja un bloque en el entorno |
| Jose  | Jose - 17/02/2025 17:24:12 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-select_f_env | 17/02/2025 17:39:45 | {"blockType": "Explore", "blockId": "85", "positionX": -82.62988, "positionY": 44.09955} | Selecciona un bloque que estaba en el entorno |
| Jose  | Jose - 17/02/2025 17:49:47 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-pause | 17/02/2025 17:49:53 | {"sbr": "..."} | Pausa el juego y la ejecución de las reglas |
| Jose  | Jose - 17/02/2025 17:49:47 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-continue | 17/02/2025 17:49:54 | {"sbr": "..."} | Reanuda el juego y ejecuta las reglas de ese instante |
| Jose  | Jose - 17/02/2025 17:24:12 | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-mod_speed | 17/02/2025 17:43:00 | {"oldSpeed": "x1", "newSpeed": "x2"} | Modifica la velocidad del juego a x1 |