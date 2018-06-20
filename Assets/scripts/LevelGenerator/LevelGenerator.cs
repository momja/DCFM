using System;
using System.Collections;
using System.Collections.Generic;
using GraphDataStructure;
using UnityEngine;

namespace LevelGenerator {

  
  public class LevelGenerator {
    List<CompoundRoom> combinedRooms;
    List<Room> allRooms;
    int diameter;
    // Constructor
    public LevelGenerator(int diameter) {
      this.combinedRooms = new List<CompoundRoom>();
      this.allRooms = initGrid(101, 10);
      this.diameter = 101;
      divideRooms(2);
      BitmapHandler.UpdateBitMap(allRooms[0], diameter);
      BitmapHandler.printMap(allRooms[0].Bitmap, diameter);
      BitmapHandler.UpdateBitMap(allRooms[1], diameter);
      BitmapHandler.printMap(allRooms[1].Bitmap, diameter);
      BitmapHandler.UpdateBitMap(allRooms[2], diameter);
      BitmapHandler.printMap(allRooms[2].Bitmap, diameter);
      BitmapHandler.UpdateBitMap(allRooms[3], diameter);
      BitmapHandler.printMap(allRooms[3].Bitmap, diameter);

      CompoundRoom newCRoom = new CompoundRoom(diameter);
      newCRoom.Add(allRooms[0]);
      newCRoom.Add(allRooms[1]);
      newCRoom.Add(allRooms[2]);
      newCRoom.Add(allRooms[3]);
      newCRoom.Add(allRooms[diameter + 1]);
      BitArray map = BitmapHandler.MergeBitmaps(newCRoom, diameter);
      BitmapHandler.printMap(map, diameter);
    }
   
    // initialise a grid of rooms.
    private List<Room> initGrid(int diameter, int gridBoxSize) {
      int width = gridBoxSize;
      int height = gridBoxSize;
      List<Room> rooms = new List<Room>();
      for(int offsetY = 0; offsetY < diameter*gridBoxSize; offsetY += gridBoxSize) {
        for(int offsetX = 0; offsetX < diameter*gridBoxSize; offsetX += gridBoxSize) {
          rooms.Add(new Room(offsetX, offsetY, width, height, diameter));
        }
      }
      return rooms;
    }

    private void divideRooms(int iterations) {
      if (iterations > 10) {
        int random = 0;
        List<Room> tempRooms = new List<Room>(allRooms);
        foreach(Room room in tempRooms) {
          random = UnityEngine.Random.Range(0,1);
          if (random == 0) {
            random = UnityEngine.Random.Range(0,1);
            List<Room> rooms = new List<Room>();
            if (random == 0) {
              rooms.AddRange(room.SplitRoom('H'));
            }
            else {
              rooms.AddRange(room.SplitRoom('V'));
            }
            // remove the old room and replace it with two new rooms
            allRooms.Remove(room);
            allRooms.AddRange(rooms);
          }
        }
        // recursively call the function x number of times
        iterations--;
        divideRooms(iterations);
      }
    }

    private BitArray mergeCombinedRooms(List<CompoundRoom> combinedRooms, int diameter) {
      // Take the list of a list room, and create a bitmap using the AND operator to combine them all
      BitArray combinedBitmap = new BitArray(diameter*diameter);
      foreach (CompoundRoom compoundRoom in combinedRooms) {
        combinedBitmap.And(compoundRoom.Bitmap);
      }
      return combinedBitmap;
    } 

    private class Room {
      public Vertex[] vertices = new Vertex[4];
      public List<Room> adjacentRooms = new List<Room>();
      public int diameter;
      public BitArray Bitmap;
      public Vertex BOTTOM_LEFT {
        get {
          return vertices[0];
        }
      }
      public Vertex BOTTOM_RIGHT {
        get {
          return vertices[1];
        }
      }
      public Vertex TOP_RIGHT {
        get {
          return vertices[2];
        }
      }
      public Vertex TOP_LEFT {
        get {
          return vertices[3];
        }
      }

      // Constructors
      public Room(Vertex[] vertices, int diameter) {
        this.vertices = vertices;
        this.diameter = diameter;
        this.Bitmap = new BitArray(diameter*diameter);
      }

      public Room(int x, int y, int width, int height, int diameter) {
        // Creates a series of points in clockwise order and assigns it to the vertices property
        Vertex[] vertices = Vertex.MakeVertexSet(new int[4,2] {{x,y},{x+width, y},{x+width, y+height},{x, y+height}});
        this.vertices = vertices;
        this.diameter = diameter;
        this.Bitmap = new BitArray(diameter*diameter);
      }

      public List<Room> SplitRoom(char splitOrientation) {
        Vertex splitPointA, splitPointB;
        Room newRoom;
        if(splitOrientation == 'V') {
          if (getWidth() <= 4) { return null; }
          //int randomNumberRange = random.Next(2, getWidth() - 2);
          splitPointA = new Vertex(BOTTOM_LEFT.x + getWidth()/2, BOTTOM_LEFT.y);
          splitPointB = new Vertex(TOP_RIGHT.x - getWidth()/2, TOP_RIGHT.y);
          Vertex[] newVertices = new Vertex[4];
          newVertices[0] = new Vertex(splitPointA);
          newVertices[1] = new Vertex(BOTTOM_RIGHT);
          newVertices[2] = new Vertex(TOP_RIGHT);
          newVertices[3] = new Vertex(splitPointB);

          newRoom = new Room(newVertices, diameter);
          this.vertices[1].x = splitPointA.x;
          this.vertices[2].x = splitPointB.x;
        }
        else if(splitOrientation == 'H') {
          Debug.Log(getHeight());
          if (getHeight() <= 4) { return null; }
          //int randomNumberRange = random.Next(2, getHeight() - 2);
          splitPointA = new Vertex(TOP_RIGHT.x, TOP_RIGHT.y - getHeight()/2);
          splitPointB = new Vertex(TOP_LEFT.x, TOP_LEFT.y - getHeight()/2);
          Vertex[] newVertices = new Vertex[4];
          newVertices[0] = new Vertex(splitPointB);
          newVertices[1] = new Vertex(splitPointA);
          newVertices[2] = new Vertex(TOP_RIGHT);
          newVertices[3] = new Vertex(TOP_LEFT);

          newRoom = new Room(newVertices, diameter);
          this.vertices[3].y = splitPointA.y;
          this.vertices[2].y = splitPointB.y;
        }
        else {
          return null;
        }
        List<Room> rooms = new List<Room>();
        rooms.Add(this);
        rooms.Add(newRoom);
        return rooms;
      }

      public int getWidth() {
        int width = BOTTOM_RIGHT.x - BOTTOM_LEFT.x;
        return width;
      }

      public int getHeight() {
        int height = TOP_RIGHT.y - BOTTOM_LEFT.y;
        return height;
      }
    }

    private class Vertex {
      // Properties
      public int x;
      public int y;

      // Constructor
      public Vertex(int x, int y) {
        this.x = x;
        this.y = y;
      }

      public Vertex(Vertex vertex) {
        this.x = vertex.x;
        this.y = vertex.y;
      }

      // Static Functions
      public static Vertex[] MakeVertexSet(int[,] coordinates) {
        Vertex[] vertexSet = new Vertex[4];
        vertexSet[0] = new Vertex(coordinates[0, 0], coordinates[0, 1]);
        vertexSet[1] = new Vertex(coordinates[1, 0], coordinates[1, 1]);
        vertexSet[2] = new Vertex(coordinates[2, 0], coordinates[2, 1]);
        vertexSet[3] = new Vertex(coordinates[3, 0], coordinates[3, 1]);
        return vertexSet;
      }

      public override String ToString() {
        return "x: " + x + ", y: " + y;
      }
    }

    private class CompoundRoom : IEnumerable<Room> {

      // Properties
      public List<Room> rooms;
      public BitArray Bitmap {
        get {
          return BitmapHandler.MergeBitmaps(this, diameter);
        }
      }     

      private int diameter; 

      // Constructor
      public CompoundRoom(int diameter) {
        rooms = new List<Room>();
        this.diameter = diameter;
      }

      public IEnumerator<Room> GetEnumerator() {
        foreach(Room room in rooms) {
          yield return room;
        }
      }

      IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
      }

      public Room this[int index] {
        get {
          return rooms[index];
        }
        set {
          rooms[index] = value;
        }
      }

      // Functions
      public void Add(Room room) {
        if(rooms.Contains(room)) {return;}
        rooms.Add(room);
      }

      public void Add(List<Room> rooms) {
        rooms.AddRange(rooms);
      }

      public void Remove(Room room) {
        if(!rooms.Contains(room)) {return;}
        rooms.Remove(room);
      }
    }

    private static class BitmapHandler {

      private static int getbitmap_index(Vertex vertex, int diameter) {
        return (vertex.x) + vertex.y*diameter;
      }

      /* public static void UpdateBitMap(BitArray bitmap, CompoundRoom CRoom, int diameter) {
        foreach(Room room in CRoom) {
          Vertex[] vertices = room.vertices;
          bitmap.SetAll(false);
          for (int i = getbitmap_index(vertices[0], diameter); i < getbitmap_index(vertices[1], diameter); i++) {
            bitmap[i] = true;
          }
          for (int i = getbitmap_index(vertices[1], diameter); i < getbitmap_index(vertices[2], diameter); i+=diameter) {
            bitmap[i] = true;
          }
          for (int i = getbitmap_index(vertices[2], diameter); i > getbitmap_index(vertices[3], diameter); i--) {
            bitmap[i] = true;
          }
          for (int i = getbitmap_index(vertices[3], diameter); i > getbitmap_index(vertices[0], diameter); i-=diameter) {
            bitmap[i] = true;
          }
        }
      } */

      public static void UpdateBitMap(Room room, int diameter) {
        Vertex[] vertices = room.vertices;
        room.Bitmap.SetAll(false);
        for (int i = getbitmap_index(vertices[0], diameter); i < getbitmap_index(vertices[1], diameter); i++) {
          room.Bitmap[i] = true;
        }
        for (int i = getbitmap_index(vertices[1], diameter); i < getbitmap_index(vertices[2], diameter); i+=diameter) {
          room.Bitmap[i] = true;
        }
        for (int i = getbitmap_index(vertices[2], diameter); i > getbitmap_index(vertices[3], diameter); i--) {
          room.Bitmap[i] = true;
        }
        for (int i = getbitmap_index(vertices[3], diameter); i > getbitmap_index(vertices[0], diameter); i-=diameter) {
          room.Bitmap[i] = true;
        }
      }

      public static void printMap(BitArray bitmap, int diameter) {
        int i = 1;
        String map = "";
        foreach (bool bit in bitmap) {
          if (bit == false) {
            map += (" 8 ");
          }
          else if (bit == true) {
            map += (" 1 ");
          }
          if (i % diameter == 0) {
            map += ("\n");
          }
          i++;
        }
        Debug.Log(map);
      }

      public static BitArray MergeBitmaps(CompoundRoom CRoom, int diameter) {
        // Take the list of rooms, and create a bitmap using the XOR operator to combine them all
        // Check all vertices in each room to ensure that a corner wasn't removed from the XOR operation
        BitArray combinedBitmap = new BitArray(CRoom[0].Bitmap.Length);
        foreach (Room room in CRoom) {
          UpdateBitMap(room, diameter);
          combinedBitmap.Xor(room.Bitmap);
          foreach (Vertex vertex in room.vertices) {
            int pointOnBitmap = getbitmap_index(vertex, room.diameter);
            if (combinedBitmap[pointOnBitmap] == false) {
              if (combinedBitmap[pointOnBitmap + 1] || combinedBitmap[pointOnBitmap - 1] || combinedBitmap[pointOnBitmap + room.diameter] || combinedBitmap[pointOnBitmap - room.diameter]) {
                combinedBitmap[pointOnBitmap] = true;
              }
            }
          }
        }

        return combinedBitmap;
      }
    }
  }

  public class SpanningTreeAlgorithm {

    public SpanningTreeAlgorithm(Graph completeGraph) {
      int[,] adjacencyMatrix = new int[completeGraph.Vertices.Length, completeGraph.Vertices.Length];
      int i = 0;
      List<int> usedVertices = new List<int>();
      //continue the loop until every vertex is included, completing the spanning tree
      makeSpanningTree(adjacencyMatrix, usedVertices, i, completeGraph);
    }

    public int[,] makeSpanningTree(int[,] adjacencyMatrix, List<int> usedVertices, int i, Graph completeGraph) {
      if (usedVertices.Count == completeGraph.Vertices.Length) {
        return adjacencyMatrix;
      }
      int[] adjacents = completeGraph.getAdjecents(i);
      int randomAdjacent;
      randomAdjacent = getRandom(adjacents, adjacencyMatrix);
      adjacencyMatrix[i, randomAdjacent] = 1;
      adjacencyMatrix[randomAdjacent, i] = 1;
      int[] randomAdjacentAdjacents = completeGraph.getAdjecents(randomAdjacent);
      int adjacentAdjacentCount = 0;
      foreach(int adjacent in randomAdjacentAdjacents) {
        if (usedVertices.Contains(adjacent) == false) {
          // an adjacent isn't already used
          adjacentAdjacentCount = 1;
          break;
        }
      }
      // if the vertex only has the one edge to the previous vertex, or it only has edges to used vertices
      if(adjacentAdjacentCount == 0) {
        // we've worked ourselves into a corner, so we better back up
        int adjacentCount = 0;
        foreach(int adjacent in adjacents) {
          if (usedVertices.IndexOf(adjacent) == -1) {
            // an adjacent isn't already used
            adjacentCount = 1;
            break;
          }
        }
        if (adjacentCount == 0) {
          i = usedVertices[0];
          usedVertices.Add(i);
        }
      }
      else {
        i = randomAdjacent;
      }
      usedVertices.Add(randomAdjacent);
      return makeSpanningTree(adjacencyMatrix, usedVertices, i, completeGraph);
    }

    public int getRandom(int[] adjacents, int[,] adjacencyMatrix) {
      System.Random random = new System.Random();
      int randomAdjacent = random.Next(0, adjacents.Length);
      // make sure the vertex isn't already in the
      for(int i = 0; i < adjacencyMatrix.Length; i++) {
        if (adjacencyMatrix[i, randomAdjacent] != 1) {
          return getRandom(adjacents, adjacencyMatrix);
        }
      }
      return randomAdjacent;
    }
  }
}