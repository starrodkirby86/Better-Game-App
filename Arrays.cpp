/* Algorithm initializes a 2D array then decides if there'll be a stair or a door to clear the room.

If stair, randomize and make sure stair is accessible
- Set all edges as walls, randomize pillars or tiles. Add a stair randomly,
override a random adjacent tile next to stair into a floor tile to ensure the stair is accessible
from at least one side


If door, randomize, then put door at edge of map
- Set all edges as walls, randomize pillars or tiles, then choose a wall tile, replace with door.
Override the adjacent non-wall tile next to door into a floor tile to ensure the door is accessible */

#include <iostream>
#include <stdlib.h>
#include <time.h>   

using namespace std;

int main()
{
	//initialize the matrix
	int** Matrix;						//A pointer to pointers to an int.
	int rows, columns;
	char type;
	cout << "Enter number of rows: ";
	cin >> rows;
	cout << "Enter number of columns: ";
	cin >> columns;
	cout << "Door or stair? (D/S) ";
	cin >> type;
	Matrix = new int*[rows];				//Matrix is now a pointer to an array of 'rows' pointers.
											//define the matrix
	srand(time(NULL));

	for (int i = 0; i<rows; i++)
	{
		Matrix[i] = new int[columns];		//the ith array is initialized
		for (int j = 0; j<columns; j++)		//the i,jth element is defined
		{
			if (j == 0 || i == 0 || j == columns -1 || i == rows -1) {
				Matrix[i][j] = 0;
			}
			else {
				Matrix[i][j] = 3;
			}
		}
	}

	if (type == 'd' || type == 'D') {
		//door algo

		for (int i = 1; i < rows-1; i++) {
			for (int j = 1; j < columns-1; j++) {
				if (j != 0 || i != 0 || j != (columns -1) || i != (rows -1)) {
					double val = (double)rand() / RAND_MAX;
					int random;
					if (val < 0.85) {      //85% chance of generating tile 2
						random = 2;
					}
					else {
						random = 3;
					}

					Matrix[i][j] = random;
				}
			}
		}

		int where, place;
		where = rand() % 2;
		cout << "Where value is: " << where << endl;
		if (where == 0) {
			place = rand() % rows;
			while (place == 0 || place == rows - 1) {
				place = rand() % rows;
			}
			int colwhere = rand() % 2;
			if (colwhere == 0) {
				Matrix[place][0] = 1;
				Matrix[place][1] = 2; //override the adjacent non-wall tile next to door into floor tile
			}
			else {
				Matrix[place][columns - 1] = 1;
				Matrix[place][columns - 2] = 2; //override the adjacent non-wall tile next to door into floor tile
			}
		}
		else {
			place = rand() % columns;
			while (place == 0 || place == columns - 1) {
				place = rand() % columns;
			}
			int rowwhere = rand() % 2;
			if (rowwhere == 0) {
				Matrix[0][place] = 1;
				Matrix[1][place] = 2; //override the adjacent non-wall tile next to door into floor tile
			}
			else {
				Matrix[rows - 1][place] = 1;
				Matrix[rows - 2][place] = 2; //override the adjacent non-wall tile next to door into floor tile
			}
		}
	}

	else if (type == 's' || type == 'S') {
		//stair algo
		for (int i = 1; i < rows - 1; i++) {
			for (int j = 1; j < columns - 1; j++) {
				if (j != 0 || i != 0 || j != (columns - 1) || i != (rows - 1)) {
					double val = (double)rand() / RAND_MAX;
					int random;
					if (val < 0.85) {      //85% chance of generating tile 2
						random = 2;
					}
					else {
						random = 3;
					}

					Matrix[i][j] = random;
				}
			}
		}

		int i = rand() % rows; //randomizes the row index for the stair
		while (i == 0 || i == rows - 1) {
			i = rand() % rows;
		}

		int j = rand() % columns; //randomizes the column index for the stair
		while (j == 0 || j == columns - 1) {
			j = rand() % columns;
		}

		Matrix[i][j] = 5;
		int space = rand() % 4 + 1;
		while (space > 0 && space <= 4) { /*loop overrides one adjacent tile next to stair as a floor tile, 
										  checking first if the selected tile next to stair is a wall*/
			cout << "Space value is : " << space << endl;
			if (space == 1) {
				if (i - 1 != 0) {
					Matrix[i - 1][j] = 2;
					break;
				}
				else {
					space = rand() % 4 + 1;
					continue;
				}
			}
			else if (space == 2) {
				if ((i + 1) != (rows - 1)) {
					Matrix[i + 1][j] = 2;
					break;
				}
				else {
					space = rand() % 4 + 1;
					continue;
				}
			}
			else if (space == 3) {
				if (j - 1 != 0) {
					Matrix[i][j - 1] = 2;
					break;
				}
				else {
					space = rand() % 4 + 1;
					continue;
				}
			}
			else if (space == 4) {
				if (j + 1 != columns - 1) {
					Matrix[i][j + 1] = 2;
					break;
				}
				else {
					space = rand() % 4 + 1;
					continue;
				}
			}
		}
	}

	//Print the matrix
	cout << "The matrix you have input is:\n";
	for (int i = 0; i<rows; i++)
	{
		for (int j = 0; j<columns; j++)
			cout << Matrix[i][j] << "\t";	//tab between each element
		cout << "\n";						//new row
	}


	//now, we have to free up the memory we took by releasing each vector:
	for (int i = 0; i<rows; i++)
		delete[] Matrix[i];				//c++ can delete up to 1 dimensional vectors

	system("pause");
}
