# -*- coding: utf-8 -*-
"""
Created on Sun Oct  8 01:43:29 2023

@author: Ernesto Amaral
"""

import socket

HOST = '192.168.100.10'  # Dirección IP del servidor
PORT = 8000  # Puerto del servidor

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    
    # Recibir el menú del servidor
    menu = s.recv(1024).decode()
    print("Menú de aplicaciones remotas:")
    print(menu)
    
    # Solicitar la selección al cliente
    selection = input("Seleccione una aplicación (1-5): ")
    
    # Enviar la selección al servidor
    s.send(selection.encode())
    
    # Recibir y mostrar el resultado
    result = s.recv(1024).decode()
    print(result)
