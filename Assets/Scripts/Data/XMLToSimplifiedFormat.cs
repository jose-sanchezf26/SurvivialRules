using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

// Clase para probar el procesado del entorno de los bloques, para transformar el formato de XML a uno más legible
public class XMLToSimplifiedFormat : MonoBehaviour
{
    // Función que convierte un string XML a un formato más simplificado
    public string ConvertXMLToSimplifiedFormat(string xmlString)
    {
        try
        {
            // Eliminar todos los caracteres '#' del XML
            xmlString = xmlString.Replace("#", "");

            // Eliminar espacios en blanco antes de la raíz y otros caracteres no válidos
            xmlString = xmlString.Trim();

            // Asegurarse de que no haya nada antes del primer bloque
            if (!xmlString.StartsWith("<Block>"))
            {
                throw new Exception("El XML no comienza con la etiqueta <Block>");
            }

            // Cargar el XML desde el string
            XDocument xmlDocument = XDocument.Parse(xmlString);

            // Inicializar una lista para almacenar los resultados
            List<string> result = new List<string>();

            // Llamar a la función recursiva para procesar los bloques
            ProcessBlock(xmlDocument.Root, "", result);

            // Unir los resultados en una cadena
            return string.Join("\n", result);
        }
        catch (Exception ex)
        {
            // Si hay un error en el XML, mostrar el mensaje
            Debug.LogError("Error al procesar el XML: " + ex.Message);
            return string.Empty;
        }
    }

    // Función recursiva que procesa cada bloque en el XML y lo convierte en formato simplificado
    private void ProcessBlock(XElement blockElement, string indentation, List<string> result)
    {
        // Extraer el nombre del bloque
        string blockName = blockElement.Element("blockName")?.Value ?? "Unnamed Block";

        // Extraer el ID (si está presente, de otro modo se pone un valor vacío o genérico)
        string blockID = blockElement.Element("defineID")?.Value ?? "";

        // Crear la línea en formato simplificado con el bloque y el ID
        string blockDescription = $"{indentation}{blockName} () ID = {blockID}";

        // Revisar si el bloque tiene inputs y agregarlos
        XElement sectionsElement = blockElement.Element("sections");
        if (sectionsElement != null)
        {
            XElement sectionElement = sectionsElement.Element("Section");
            if (sectionElement != null)
            {
                XElement inputsElement = sectionElement.Element("inputs");
                if (inputsElement != null)
                {
                    foreach (XElement inputElement in inputsElement.Elements("Input"))
                    {
                        string value = inputElement.Element("value")?.Value ?? "";
                        if (!string.IsNullOrEmpty(value))
                        {
                            blockDescription += $" ({value})";
                        }

                        // Revisar si el input tiene una operación
                        XElement operationElement = inputElement.Element("operation");
                        if (operationElement != null)
                        {
                            string operationBlock = ProcessOperation(operationElement);
                            blockDescription += $" ({operationBlock})";
                        }
                    }
                }
            }
        }


        // Añadir el bloque procesado al resultado
        result.Add(blockDescription);

        // Procesar los bloques hijos si existen
        if (sectionsElement != null)
        {
            foreach (XElement sectionElement in sectionsElement.Elements("Section"))
            {
                XElement childBlocksElement = sectionElement.Element("childBlocks");
                if (childBlocksElement != null)
                {
                    foreach (XElement childBlockElement in childBlocksElement.Elements("Block"))
                    {
                        // Llamar recursivamente para procesar el bloque hijo
                        ProcessBlock(childBlockElement, indentation + "    ", result); // Añadir tabulación para hijos
                    }
                }
            }
        }
    }

    // Función que procesa las operaciones dentro de un bloque (inputs con operación)
    private string ProcessOperation(XElement operationElement)
    {
        // Aquí procesamos un bloque de operación (p. ej., suma, resta, etc.)
        XElement blockElement = operationElement.Element("Block");
        if (blockElement != null)
        {
            // Llamar recursivamente para procesar el bloque de operación
            string blockName = blockElement.Element("blockName")?.Value ?? "Unnamed Block";
            string blockID = blockElement.Element("defineID")?.Value ?? "";

            // Si el bloque tiene inputs, procesarlos de la misma manera
            XElement inputsElement = blockElement.Element("inputs");
            string inputs = "";
            if (inputsElement != null)
            {
                foreach (XElement inputElement in inputsElement.Elements("Input"))
                {
                    string value = inputElement.Element("value")?.Value ?? "";
                    if (!string.IsNullOrEmpty(value))
                    {
                        inputs += $" ({value})";
                    }

                    // Revisar si el input tiene una operación y procesarla
                    XElement operationChildElement = inputElement.Element("operation");
                    if (operationChildElement != null)
                    {
                        inputs += $" ({ProcessOperation(operationChildElement)})";
                    }
                }
            }

            return $"{blockName} ({inputs}) ID = {blockID}";
        }

        return "";
    }

    // Esto es un ejemplo de cómo llamar a la función
    void Start()
    {
        // Ejemplo de código XML en formato string (el ejemplo que proporcionaste)
        string xmlString = @"<Block>
                              <blockName>Block Ins WhenPlayClicked</blockName>
                              <position>(42.73, -311.05, 0.00)</position>
                              <varManagerName></varManagerName>
                              <varName />
                              <defineID />
                              <isLocalVar />
                              <defineItems />
                              <sections>
                                <Section>
                                  <childBlocks>
                                    <Block>
                                      <blockName>Block Cst Rules</blockName>
                                      <position>(0.00, 10.00, 0.00)</position>
                                      <varManagerName></varManagerName>
                                      <varName />
                                      <defineID />
                                      <isLocalVar />
                                      <defineItems />
                                      <sections>
                                        <Section>
                                          <childBlocks>
                                            <Block>
                                              <blockName>Block Cst RuleWithID</blockName>
                                              <position>(20.00, 10.00, 0.00)</position>
                                              <varManagerName></varManagerName>
                                              <varName />
                                              <defineID />
                                              <isLocalVar />
                                              <defineItems />
                                              <sections>
                                                <Section>
                                                  <childBlocks>
                                                    <Block>
                                                      <blockName>Block Cst SetTarget</blockName>
                                                      <position>(20.00, 10.00, 0.00)</position>
                                                      <varManagerName></varManagerName>
                                                      <varName />
                                                      <defineID />
                                                      <isLocalVar />
                                                      <defineItems />
                                                      <sections>
                                                        <Section>
                                                          <childBlocks />
                                                          <inputs>
                                                            <Input>
                                                              <isOperation>false</isOperation>
                                                              <value>Well</value>
                                                            </Input>
                                                          </inputs>
                                                        </Section>
                                                      </sections>
                                                    </Block>
                                                  </childBlocks>
                                                  <inputs />
                                                </Section>
                                              </sections>
                                            </Block>
                                          </childBlocks>
                                          <inputs>
                                            <Input>
                                              <isOperation>true</isOperation>
                                              <value>0</value>
                                              <operation>
                                                <Block>
                                                  <blockName>Block Op BiggerThan</blockName>
                                                  <position>(-198.35, 35.00, 0.00)</position>
                                                  <varManagerName></varManagerName>
                                                  <varName />
                                                  <defineID />
                                                  <isLocalVar />
                                                  <defineItems />
                                                  <sections>
                                                    <Section>
                                                      <childBlocks />
                                                      <inputs>
                                                        <Input>
                                                          <isOperation>true</isOperation>
                                                          <value>6</value>
                                                          <operation>
                                                            <Block>
                                                              <blockName>Block Op Divide</blockName>
                                                              <position>(-160.76, 25.00, 0.00)</position>
                                                              <varManagerName></varManagerName>
                                                              <varName />
                                                              <defineID />
                                                              <isLocalVar />
                                                              <defineItems />
                                                              <sections>
                                                                <Section>
                                                                  <childBlocks />
                                                                  <inputs>
                                                                    <Input>
                                                                      <isOperation>false</isOperation>
                                                                      <value>12</value>
                                                                    </Input>
                                                                    <Input>
                                                                      <isOperation>false</isOperation>
                                                                      <value>2</value>
                                                                    </Input>
                                                                  </inputs>
                                                                </Section>
                                                              </sections>
                                                            </Block>
                                                          </operation>
                                                        </Input>
                                                        <Input>
                                                          <isOperation>false</isOperation>
                                                          <value>6</value>
                                                        </Input>
                                                      </inputs>
                                                    </Section>
                                                  </sections>
                                                </Block>
                                              </operation>
                                            </Input>
                                          </inputs>
                                        </Section>
                                      </sections>
                                    </Block>
                                  </childBlocks>
                                  <inputs />
                                </Section>
                              </sections>
                            </Block>";

        // Llamar al método para convertir el XML a formato simplificado
        string simplifiedCode = ConvertXMLToSimplifiedFormat(xmlString);

        // Mostrar el resultado
        Debug.Log(simplifiedCode);
    }
}
