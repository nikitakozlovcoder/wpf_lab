using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6
{
    //Отношение между 2 треугольниками
    enum Relation
    {
        Smaller,
        Equal,
        Bigger,
        Incorrect,
        Same //обозначает сравнение объекта с самим собой (при условии установки флага checkForSame)

    }
}
