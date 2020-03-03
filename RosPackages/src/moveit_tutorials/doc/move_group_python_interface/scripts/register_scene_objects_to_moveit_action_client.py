#! /usr/bin/env python

import rospy
import actionlib
import moveit_tutorials.msg
import moveit_commander
import geometry_msgs.msg
import random

def register_scene_objects_to_moveit_action_client():
    client = actionlib.SimpleActionClient('register_scene_objects_to_moveit', moveit_tutorials.msg.RegisterSceneObjectToMoveitAction)
    client.wait_for_server()

    goal = moveit_tutorials.msg.RegisterSceneObjectToMoveitGoal()
    for i in range(1):
        goal.positions_x.append(random.random())
        goal.positions_y.append(random.random())
        goal.positions_z.append(random.random())

        goal.orientations_x.append(random.random())
        goal.orientations_y.append(random.random())
        goal.orientations_z.append(random.random())
        goal.orientations_w.append(random.random())

        goal.sizes_x.append(random.random())
        goal.sizes_y.append(random.random())
        goal.sizes_z.append(random.random())

        goal.names.append('box' + str(i))

    client.send_goal(goal)
    client.wait_for_result()

    return client.get_result()

if __name__ == '__main__':
    try:
        rospy.init_node('register_scene_objects_to_moveit_action_client_py')
        result = register_scene_objects_to_moveit_action_client()
        print("Result: " + result.log)
    except rospy.ROSInterruptException:
        print("program interrupted before completion")
