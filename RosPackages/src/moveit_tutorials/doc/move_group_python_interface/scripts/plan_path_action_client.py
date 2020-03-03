#! /usr/bin/env python

import rospy
import actionlib
import moveit_tutorials.msg
import moveit_commander
import geometry_msgs.msg
import random
from geometry_msgs.msg import Quaternion
from tf.transformations import quaternion_from_euler


def plan_path_action_client():
    client = actionlib.SimpleActionClient('plan_path', moveit_tutorials.msg.PlanPathAction)
    client.wait_for_server()
    
    goal = moveit_tutorials.msg.PlanPathGoal()
    
    # 'home' state
    pose_home = geometry_msgs.msg.Pose()
    pose_home.position.x    = 0.817163627025
    pose_home.position.y    = 0.191810588973
    pose_home.position.z    = -0.00557119213564
    pose_home.orientation.x = -0.706955753498
    pose_home.orientation.y = -0.707257773903
    pose_home.orientation.z = 4.38839210596e-05
    pose_home.orientation.w   = 4.38648674085e-05

    # set start position
    pose_start = geometry_msgs.msg.Pose()
    pose_start.position.x    = -0.0770189910428
    pose_start.position.y    = 0.414723797854
    pose_start.position.z    = 0.628412522349
    pose_start.orientation.x = 0.707140675673
    pose_start.orientation.y = 0.707072884451
    pose_start.orientation.z = -2.27854326042e-05
    pose_start.orientation.w = 1.90573273226e-05

    # set waypoint position
    pose_1 = geometry_msgs.msg.Pose()
    pose_1.position.x    = -0.2770189910428
    pose_1.position.y    = 0.414723797854
    pose_1.position.z    = 0.628412522349
    pose_1.orientation.x = 0.707140675673
    pose_1.orientation.y = 0.707072884451
    pose_1.orientation.z = -2.27854326042e-05
    pose_1.orientation.w = 1.90573273226e-05  
    
    pose_2 = geometry_msgs.msg.Pose()
    pose_2.position.x    = 0.2770189910428
    pose_2.position.y    = 0.414723797854
    pose_2.position.z    = 0.628412522349
    pose_2.orientation.x = 0.707140675673
    pose_2.orientation.y = 0.707072884451
    pose_2.orientation.z = -2.27854326042e-05
    pose_2.orientation.w = 1.90573273226e-05  

    pose_3 = geometry_msgs.msg.Pose()
    pose_3.position.x    = 0.2770189910428
    pose_3.position.y    = 0.614723797854
    pose_3.position.z    = 0.628412522349
    pose_3.orientation.x = 0.707140675673
    pose_3.orientation.y = 0.707072884451
    pose_3.orientation.z = -2.27854326042e-05
    pose_3.orientation.w = 1.90573273226e-05 

    pose_4 = geometry_msgs.msg.Pose()
    pose_4.position.x    = -0.2770189910428
    pose_4.position.y    = 0.614723797854
    pose_4.position.z    = 0.628412522349
    pose_4.orientation.x = 0.707140675673
    pose_4.orientation.y = 0.707072884451
    pose_4.orientation.z = -2.27854326042e-05
    pose_4.orientation.w = 1.90573273226e-05  

    # fill goal msgs    
    goal.waypoints.append(pose_start)
    goal.waypoints.append(pose_1)
    goal.waypoints.append(pose_2)
    goal.waypoints.append(pose_3)
    goal.waypoints.append(pose_4)
    goal.waypoints.append(pose_start)

    client.send_goal(goal)
    client.wait_for_result()

    return client.get_result()

if __name__ == '__main__':
    try:
        rospy.init_node('plan_path_action_client_py')
        result = plan_path_action_client()
        print("Result: " + result.log)
    except rospy.ROSInterruptException:
        print("program interrupted before completion")
