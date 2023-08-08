import React from 'react';
import { FlatList, StyleSheet, Text, View } from 'react-native';


const todoList= [
    {
        Id:0,
        Task: 'Test 0',
        IsCompleted: false
    },
    {
        Id:1,
        Task: 'Test 1',
        IsCompleted: false
    },
    {
        Id:2,
        Task: 'Test 2',
        IsCompleted: false
    },
    {
        Id:3,
        Task: 'Test 3',
        IsCompleted: false
    },
    {
        Id:4,
        Task: 'Test 4',
        IsCompleted: false
    }
]


const listItems = todoList.map((task) => 
  <li>
    <Text>
      {task.Task}
    </Text>
  </li>
);

export default function List() {
  return(
    <View>
      <FlatList data={todoList}
      renderItem={({item}) => <Text>{item.Task}</Text>}>
      </FlatList>
    </View>
  );
}